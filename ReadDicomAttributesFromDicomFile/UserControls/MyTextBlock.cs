using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReadDicomAttributesFromDicomFile.UserControls
{
    public partial class MyTextBlock:UserControl
    {
        public MyTextBlock()
        {
            //InitializeComponent();
        }

        public static readonly DependencyProperty SetTextProperty = 
         DependencyProperty.Register("SetText", typeof(string), typeof(MyTextBlock), new 
            PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged))); 
				
      public string SetText { 
         get { return (string)GetValue(SetTextProperty); } 
         set { SetValue(SetTextProperty, value); } 
      } 
		
      private static void OnSetTextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e) { 
         MyTextBlock MyTextBlockControl = d as MyTextBlock; 
         MyTextBlockControl.OnSetTextChanged(e); 
      } 
		
      private void OnSetTextChanged(DependencyPropertyChangedEventArgs e) { 
         //tbTest.Text = e.NewValue.ToString(); 
      }  
   } 
    
}
