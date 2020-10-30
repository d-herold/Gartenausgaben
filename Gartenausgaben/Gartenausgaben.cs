using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Gartenausgaben : Form
    {
        public Gartenausgaben()
        {
            InitializeComponent();
            SetMyCustomFormat();
        }
        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "ddMMMM yyyy"; //MMMM dd, yyyy";
        }        
    }
}
/*
public class MyWindow : Form
{
   //-------------------------------------------------------------
   static int iId    = 1;
   if(!SW_MAINWINDOW)
      static int iCount = 0;

   //=============================================================
   public MyWindow ()
   {
      Control ctrlCurr;


      //----------------------------------------------------------
      if(!SW_MAINWINDOW)
         ++iCount;
      Text = "Window " + iId++;
      Size = new Size (220 + Size.Width - ClientSize.Width,
                        70 + Size.Height - ClientSize.Height);

      //----------------------------------------------------------
      ctrlCurr = new Button ();
      ctrlCurr.Location = new Point (10,10);
      ctrlCurr.Size = new Size (200, 50);
      ctrlCurr.Text = "New Window";
      ctrlCurr.Click += new EventHandler (ButtonClick);
      Controls.Add (ctrlCurr);

      //----------------------------------------------------------
      if(!SW_MAINWINDOW)
      {
         Disposed += new EventHandler (MyWindowDisposed);
         Show ();
      }
   }

   //=============================================================
   protected void ButtonClick (Object sender, EventArgs e)
   {
      MyWindow myw = new MyWindow ();
   }

   //=============================================================
      if(!SW_MAINWINDOW)
      {
        protected void MyWindowDisposed (Object sender, EventArgs e)
        {
             if (--iCount <= 0)
             {
                Application.Exit ();
             }
        }
      }
}

//****************************************************************
abstract class App
{
   //=============================================================
   public static int Main (string [] astrArg)
   {
      if(!SW_MAINWINDOW)
      {
         new MyWindow ();
         Application.Run ();
      }
      else
         Application.Run (new MyWindow ());

      return 0;
   }
}
 */
