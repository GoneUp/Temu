namespace Tera.Network.old_Client
{
    class RpCraftStart : ARecvPacket
    {
        protected int RecipeId;

        public override void Read()
        {
            RecipeId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.CraftService.ProcessCraft(Connection.Player, RecipeId);
        }
    }
}
