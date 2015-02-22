using Tera.Communication;
using Tera.Communication.Logic;
using Tera.Controllers;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Geometry;
using Tera.Data.Structures.Player;
using Tera.Extensions;
using Tera.Network.old_Server;
using Utils;

namespace Tera.AiEngine
{
    class NpcAi : DefaultAi
    {
        protected const int ActionInterval = 1000;

        public NpcMoveController MoveController;

        public NpcBattleController BattleController;

        protected long LastCallUts = RandomUtilities.GetCurrentMilliseconds();

        protected long LastWalkUts;

        protected long NextChangeDirectionUts;

        protected long RespawnUts = 0;

        protected Player PlayerInFocus;

        public override void Init(Creature creature)
        {
            base.Init(creature);

            MoveController = new NpcMoveController(Npc);
            BattleController = new NpcBattleController(Npc);
        }

        public override void Release()
        {
            base.Release();

            if (MoveController != null)
                MoveController.Release();
            MoveController = null;

            if (BattleController != null)
                BattleController.Release();
            BattleController = null;
        }

        public override void OnAttacked(Creature attacker, int damage)
        {
            Npc.Target = attacker;

            BattleController.AddDamage(attacker, damage);
            BattleController.AddAggro(attacker, damage);
        }

        public override Creature GetKiller()
        {
            return BattleController.GetKiller();
        }

        public override void DealExp()
        {
            BattleController.DealExp();
        }

        //Actions:

        public override void Action()
        {
            if (Npc.NpcTemplate.IsVillager || Npc.NpcTemplate.IsObject)
                return;

            long now = RandomUtilities.GetCurrentMilliseconds();

            if (Npc.LifeStats.IsDead())
            {
                if (RespawnUts == 0)
                {
                    CreatureLogic.NpcDied(Npc);
                    RespawnUts = now + 20000;
                    return;
                }

                if (now < RespawnUts || Global.MapService.IsDungeon(Npc.Position.MapId))
                    return;

                RespawnUts = 0;
                Npc.BindPoint.CopyTo(Npc.Position);

                MoveController.Reset();
                BattleController.Reset();

                Npc.LifeStats.Rebirth();
            }

            long elapsed = now - LastCallUts;
            LastCallUts = now;

            MoveController.Action(elapsed);
            BattleController.Action();

            //if (Npc.VisiblePlayers.Count < 1) return;

            EnemiesListenAction();

            if (NextChangeDirectionUts < now)
                RandomWalkAction();
        }

        protected void EnemiesListenAction()
        {
            if (Npc.Target != null || Npc.LifeStats.IsDead())
            {
                PlayerInFocus = null;
                return;
            }

            const int agroDistance = 300;

            if (PlayerInFocus != null)
            {
                if (PlayerInFocus.Position.DistanceTo(Npc.Position) > agroDistance || PlayerInFocus.LifeStats.IsDead())
                {
                    PlayerInFocus = null;
                    Global.VisibleService.Send(Npc, new SpNpcStatus(Npc, 5, 0));
                }
            }

            if (PlayerInFocus != null)
            {
                long now = RandomUtilities.GetCurrentMilliseconds();

                if (NextChangeDirectionUts < now)
                {
                    short heading = Geom.GetHeading(Npc.Position, PlayerInFocus.Position);

                    short turnTime = (short)(2 * System.Math.Abs(Npc.Position.Heading - heading)
                            / System.Math.Max(45, System.Math.Max(Npc.NpcTemplate.Shape.TurnSpeed, Npc.NpcTemplate.Shape.WalkSpeed)));

                    if (turnTime > 200 && !MoveController.IsActive)
                    {
                        Global.VisibleService.Send(Npc, new SpDirectionChange(Creature, heading, turnTime));

                        Npc.Position.Heading = heading;
                        NextChangeDirectionUts = now + turnTime;
                    }
                    else
                        NextChangeDirectionUts = now + 500;
                }

                return;
            }

            Npc.VisiblePlayers.Each(
                player =>
                    {
                        if (PlayerInFocus != null
                            || player.LifeStats.IsDead()
                            || player.Position.DistanceTo(Npc.Position) > agroDistance)
                            return;

                        OnCreatureApproached(player);
                    });
        }

        protected void OnCreatureApproached(Player player)
        {
            PlayerInFocus = player;

            if (!BattleController.IsHateCreature(player))
            {
                new SpShowIcon(Npc, 4).Send(player.Connection);

                Global.VisibleService.Send(Npc, new SpNpcStatus(Npc, 5, 2, player));

                //if (Npc.NpcTemplate.Level >= 20)
                //    OnAttacked(creature, 0);
            }
        }

        protected void RandomWalkAction()
        {
            if (Npc.Target != null || MoveController.IsActive)
                return;

            if (Npc.Attack != null && !Npc.Attack.IsFinished)
                return;

            if (Npc.NpcTemplate.Shape.WalkSpeed <= 0)
                return;

            if (Npc.Instance.IsEditingMode)
            {
                if (Npc.Position.FastDistanceTo(Npc.BindPoint) > 0)
                    MoveController.MoveTo(Creature.BindPoint);

                return;
            }

            long now = RandomUtilities.GetCurrentMilliseconds();

            if (now - 10000 < LastWalkUts)
                return;

            LastWalkUts = RandomUtilities.GetCurrentMilliseconds() + Random.Next(5000, 10000);

            if (Random.Next(0, 100) < 50)
                return;

            double distanceToBind = Npc.BindPoint.DistanceTo(Creature.Position);
            if (distanceToBind > 500)
            {
                MoveController.MoveTo(Creature.BindPoint);
                return;
            }

            MoveController.MoveTo(Npc.Position.X
                                  + Random.Next(150, 300)
                                  * (Random.Next(0, 100) < 50 ? 1 : -1)
                                  ,
                                  Npc.Position.Y
                                  + Random.Next(150, 300)
                                  * (Random.Next(0, 100) < 50 ? 1 : -1));
        }
    }
}