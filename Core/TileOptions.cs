using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Core.SmallTile;
using Core.WideTile;
using System.Diagnostics;

namespace Core
{
    public class TileOptions
    {

        #region WhatClockFaceToPut
        public WriteableBitmap getBmp()
        {
            Settings appSettings = new Settings();
            string type = appSettings.getSettingsString(appSettings.TileType);

            if (type == "Defualt")
            {
                var customTile = new BatterySmall();
                customTile.Measure(new Size(336, 336));
                customTile.Arrange(new Rect(0, 0, 336, 336));

                var bmp = new WriteableBitmap(336, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Vertical")
            {
                var customTile = new VerticalSmall();
                customTile.Measure(new Size(336, 336));
                customTile.Arrange(new Rect(0, 0, 336, 336));

                var bmp = new WriteableBitmap(336, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Traditional")
            {
                var customTile = new TraditionalSmall();
                customTile.Measure(new Size(336, 336));
                customTile.Arrange(new Rect(0, 0, 336, 336));

                var bmp = new WriteableBitmap(336, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Domo")
            {
                var customTile = new DomoSmall();
                customTile.Measure(new Size(336, 336));
                customTile.Arrange(new Rect(0, 0, 336, 336));

                var bmp = new WriteableBitmap(336, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Simple")
            {
                var customTile = new SimpleSmall();
                customTile.Measure(new Size(336, 336));
                customTile.Arrange(new Rect(0, 0, 336, 336));

                var bmp = new WriteableBitmap(336, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            
            return new WriteableBitmap(336, 336);
        }
        #endregion

        #region WideClock
        public WriteableBitmap getWideBmp()
        {
            Settings appSettings = new Settings();
            string type = appSettings.getSettingsString(appSettings.TileType);

            if (type == "Defualt")
            {
                var customTile = new BatteryLarge();
                customTile.Measure(new Size(691, 336));
                customTile.Arrange(new Rect(0, 0, 691, 336));

                var bmp = new WriteableBitmap(691, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Vertical")
            {
                var customTile = new VerticalWide();
                customTile.Measure(new Size(691, 336));
                customTile.Arrange(new Rect(0, 0, 691, 336));

                var bmp = new WriteableBitmap(691, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Traditional")
            {
                var customTile = new TraditionalWide();
                customTile.Measure(new Size(691, 336));
                customTile.Arrange(new Rect(0, 0, 691, 336));

                var bmp = new WriteableBitmap(691, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Domo")
            {
                var customTile = new DomoWide();
                customTile.Measure(new Size(691, 336));
                customTile.Arrange(new Rect(0, 0, 691, 336));

                var bmp = new WriteableBitmap(691, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            else if (type == "Simple")
            {
                var customTile = new SimpleWide();
                customTile.Measure(new Size(691, 336));
                customTile.Arrange(new Rect(0, 0, 691, 336));

                var bmp = new WriteableBitmap(691, 365);
                bmp.Render(customTile, null);
                bmp.Invalidate();
                return bmp;
            }
            return new WriteableBitmap(697, 365);
        }
        #endregion


        public void UpdateTile()
        {
            Debug.WriteLine("Live tile updating");

            const string filename = "/Shared/ShellContent/CustomTile.png";
            const string wideFilename = "/Shared/ShellContent/CustomTileWide.png";

            //create small tile
            using (var isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isf.DirectoryExists("/CustomLiveTiles"))
                {
                    isf.CreateDirectory("/CustomLiveTiles");
                }

                using (var stream = isf.OpenFile(filename, System.IO.FileMode.OpenOrCreate))
                {
                    //bmp.SaveJpeg(stream, 336, 366, 0, 100);
                    getBmp().WritePNG(stream);
                }
                //create large tile
                using (var stream = isf.OpenFile(wideFilename, System.IO.FileMode.OpenOrCreate))
                {
                    getWideBmp().WritePNG(stream);
                }
            }

            //update
            try
            {
                FlipTileData tileData = new FlipTileData
                {
                    BackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute),
                };


                ShellTile tile = ShellTile.ActiveTiles.First();
                if (null != tile)
                {
                    foreach (var sec in ShellTile.ActiveTiles)
                    {
                        FlipTileData data = new FlipTileData();
                        // tile foreground data
                        data.BackgroundImage = new Uri("isostore:" + filename, UriKind.Absolute);
                        data.WideBackgroundImage = new Uri("isostore:" + wideFilename, UriKind.Absolute);
                        // update tile
                        sec.Update(data);
                    }
                    // create a new data for tile

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        
    }
}
