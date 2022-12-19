#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

using System.Reflection;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace Test_toolbar
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            string AssemblyName = GetAssemblyName();
            // create ribbon tab
            a.CreateRibbonTab("Laura TEST");
            a.CreateRibbonTab("Laura other TEST");

            //create ribbon panel(s)
            RibbonPanel Panel1 = a.CreateRibbonPanel("Laura TEST", "Moose dock");
            RibbonPanel Panel2 = a.CreateRibbonPanel("Laura TEST", "Awesome");
            RibbonPanel Panel3 = a.CreateRibbonPanel("Laura TEST", "Cheese plate");

            //Create button data
            PushButtonData pData1 = new PushButtonData("button1", "This is \r Button one", AssemblyName, "Test_toolbar.Command1");
            PushButtonData pData2 = new PushButtonData("button2", "button2", AssemblyName, "Test_toolbar.Command2");
            PushButtonData pData3 = new PushButtonData("cheese", "cheese", AssemblyName, "Test_toolbar.Command2");

            //Add images
           
            pData1.LargeImage = BitmapToImageSource(Test_toolbar.Properties.Resources._32x32Moose);
            pData1.Image = BitmapToImageSource(Test_toolbar.Properties.Resources._32x32Moose);
            pData2.LargeImage = BitmapToImageSource(Test_toolbar.Properties.Resources.Untitled);
            pData2.Image = BitmapToImageSource(Test_toolbar.Properties.Resources.Untitled);
            pData3.LargeImage = BitmapToImageSource(Test_toolbar.Properties.Resources.Cheese);
            pData3.Image = BitmapToImageSource(Test_toolbar.Properties.Resources.Cheese);

            //pData3.LargeImage = Base64img(Test_toolbar.Properties.Resources.InvaderString);

            //Add tooltips
            pData1.ToolTip = "Moose tip";
            pData2.ToolTip = "Button 2 tool tip";
            pData3.ToolTip = "Mmm cheese";

            //Create buttons
            Panel1.AddItem(pData1);
            Panel1.AddItem(pData2);
            Panel2.AddItem(pData3);
            Panel3.AddItem(pData3);

            //make edits to add-in file

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        private BitmapImage BitmapToImageSource(Bitmap BMapm)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                BMapm.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();

                return bmi;

            }            
        }

        /*
        private ImageSource Base64img(Bitmap BMapm)
        {
            byte[] bytes = Convert.FromBase64String(MyImage);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                pic.Image = Image.FromStream(ms);
            }

        }
        */

        private string GetAssemblyName()
        {
            string AssemblyName = Assembly.GetExecutingAssembly().Location;
            return AssemblyName;
        }
    }
}
