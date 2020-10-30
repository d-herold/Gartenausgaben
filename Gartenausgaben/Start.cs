using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Start : Form
    {
        Invoice invoice = new Invoice();
        
        public Start()
        {
            InitializeComponent();
        }

        private void CmdLoadNewInvoice_Click(object sender, EventArgs e)
        {
            if ((invoice.IsDisposed) || (null == invoice))
                invoice = new Invoice();
            invoice.Show();
            this.Close();
        }

        private void CmdEvaluation_Click(object sender, EventArgs e)
        {
            var evaluation = new Evaluation();
            evaluation.Show();
        }

    }
}

/*
public class MyWindow : Form
{
   //-------------------------------------------------------------
   static int iId    = 1;
   #if !SW_MAINWINDOW
      static int iCount = 0;
   #endif

   //=============================================================
   public MyWindow ()
   {
      Control ctrlCurr;


      //----------------------------------------------------------
      #if !SW_MAINWINDOW
         ++iCount;
      #endif
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
      #if !SW_MAINWINDOW
         Disposed += new EventHandler (MyWindowDisposed);
         Show ();
      #endif
   }

   //=============================================================
   protected void ButtonClick (Object sender, EventArgs e)
   {
      MyWindow myw = new MyWindow ();
   }

   //=============================================================
   #if !SW_MAINWINDOW
      protected void MyWindowDisposed (Object sender, EventArgs e)
      {
         if (--iCount <= 0) {
            Application.Exit ();
         }
      }
   #endif
}

//****************************************************************
abstract class App
{
   //=============================================================
   public static int Main (string [] astrArg)
   {
      #if !SW_MAINWINDOW
         new MyWindow ();
         Application.Run ();
      #else
         Application.Run (new MyWindow ());
      #endif

      return 0;
   }
}
 */
