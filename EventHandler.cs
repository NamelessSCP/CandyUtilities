namespace CandyUtilities.Events
{
    using CandyUtilities;
    using Exiled.Events.EventArgs.Scp330;
    using InventorySystem.Items.Usables.Scp330;
    using Exiled.API.Features;
    public sealed class EventHandler
    {
        Config config = CandyUtil.Instance.Config;
        Random random = new Random();
        public void OnInteraction(InteractingScp330EventArgs ev)
        {
            if(!ev.IsAllowed) return;
            if (random.Next(1, 101) <= config.PinkChance)
            {
                Log.Debug("Pink candy has been selected!");
                ev.Candy = CandyKindID.Pink;
            }
            if(!config.ShouldShowHint || config.TextOnPickup.Length == 0) return;
            List<KeyValuePair<string, string>> candies = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Rainbow", "<color=#FF0000>R</color><color=#FF7F00>a</color><color=#FFFF00>i</color><color=#00FF00>n</color><color=#0000FF>b</color><color=#4B0082>o</color><color=#8A2BE2>w</color>"),
                new KeyValuePair<string, string>("Yellow", "<color=#FFFF00>Yellow</color>"),
                new KeyValuePair<string, string>("Purple", "<color=#800080>Purple</color>"),
                new KeyValuePair<string, string>("Red", "<color=#FF0000>Red</color>"),
                new KeyValuePair<string, string>("Green", "<color=#008000>Green</color>"),
                new KeyValuePair<string, string>("Blue", "<color=#0000FF>Blue</color>"),
                new KeyValuePair<string, string>("Pink", "<color=#FFC0CB>Pink</color>")
            };
            string currentCandy = ev.Candy.ToString();
            foreach (var candy in candies) currentCandy = currentCandy.Replace(candy.Key, candy.Value);
            string text = config.TextOnPickup.Replace("%type%", currentCandy);
            ev.Player.ShowHint(text);
            Log.Debug(text);
        }
    }
}