﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace NPCMapLocations
{
    // Mod slider for heart level this.Config
    internal class MapModSlider : OptionsElement
    {
        private readonly int max;
        private int min;
        private float value;
        public string valueLabel;

        public MapModSlider(
            string label,
            int whichOption,
            int x,
            int y,
            int min,
            int max
        ) : base(label, x, y, 48 * Game1.pixelZoom, 6 * Game1.pixelZoom, whichOption)
        {
            this.min = min;
            this.max = max;
            if (whichOption != 8 && whichOption != 9) this.bounds.Width = this.bounds.Width * 2;
            this.valueLabel = ModMain.Helper.Translation.Get(label);

            switch (whichOption)
            {
                case 8:
                    this.value = ModMain.Config.HeartLevelMin;
                    break;
                case 9:
                    this.value = ModMain.Config.HeartLevelMax;
                    break;
                default:
                    break;
            }
        }

        public override void leftClickHeld(int x, int y)
        {
            if (this.greyedOut)
                return;

            base.leftClickHeld(x, y);
            this.value = x >= this.bounds.X
                ? (x <= this.bounds.Right - 10 * Game1.pixelZoom
                    ? (int)((x - this.bounds.X) / (float)(this.bounds.Width - 10 * Game1.pixelZoom) * this.max)
                    : this.max)
                : 0;

            switch (this.whichOption)
            {
                case 8:
                    ModMain.Config.HeartLevelMin = (int)this.value;
                    break;
                case 9:
                    ModMain.Config.HeartLevelMax = (int)this.value;
                    break;
                default:
                    break;
            }

            ModMain.Helper.Data.WriteJsonFile($"config/{Constants.SaveFolderName}.json", ModMain.Config);
        }

        public override void receiveLeftClick(int x, int y)
        {
            if (this.greyedOut) return;

            base.receiveLeftClick(x, y);
            this.leftClickHeld(x, y);
        }

        public override void draw(SpriteBatch b, int slotX, int slotY, IClickableMenu context = null)
        {
            this.label = this.valueLabel + ": " + this.value;
            this.greyedOut = false;
            if (this.whichOption == 8 || this.whichOption == 9) this.greyedOut = !ModMain.Config.ByHeartLevel;

            base.draw(b, slotX, slotY, context);
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, OptionsSlider.sliderBGSource, slotX + this.bounds.X,
                slotY + this.bounds.Y, this.bounds.Width, this.bounds.Height, Color.White, Game1.pixelZoom, false);
            b.Draw(Game1.mouseCursors,
                new Vector2(
                    slotX + this.bounds.X + (this.bounds.Width - 10 * Game1.pixelZoom) *
                    (this.value / this.max), slotY + this.bounds.Y),
                OptionsSlider.sliderButtonRect, Color.White, 0f, Vector2.Zero, Game1.pixelZoom,
                SpriteEffects.None, 0.9f);
        }
    }
}
