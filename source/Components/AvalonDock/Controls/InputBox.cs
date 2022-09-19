using AvalonDock.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AvalonDock.Controls
{
	public class InputBox
	{
		private Window _box = new Window();//window for the inputbox
		private int _fontSize = 30;//fontsize for the input
		private StackPanel _sp1 = new StackPanel();// items container
		private StackPanel _sp2 = new StackPanel();
		private string _boxcontent;//title
		private string _defaulttext = "";//default textbox content
		private string _errormessage = "Invalid answer";//error messagebox content
		private string _errortitle = "Error";//error messagebox heading title
		private string _okbuttontext = "OK";//Ok button content
		private bool _clicked = false;
		private TextBox _input = new TextBox();
		private Button _ok = new Button();
		private Button _cancel = new Button();
		private bool _inputreset = false;

		public InputBox(string content)
		{
			try
			{
				_boxcontent = content;
			}
			catch { _boxcontent = "Error!"; }
			windowdef();
		}

		public InputBox(string content, string DefaultText)
		{
			try
			{
				_boxcontent = content;
			}
			catch { _boxcontent = "Error!"; }

			try
			{
				_defaulttext = DefaultText;
			}
			catch
			{
				DefaultText = "Error!";
			}
			windowdef();
		}

		public InputBox(string content, int Fontsize)
		{
			try
			{
				_boxcontent = content;
			}
			catch { _boxcontent = "Error!"; }

			if (Fontsize >= 1)
				_fontSize = Fontsize;
			windowdef();
		}

		private void windowdef()// window building - check only for window size
		{
			_box.Height = 140;// Box Height
			_box.Width = 300;// Box Width
			_box.Title = Resources.Document_Rename;
			_box.Content = _sp1;
			_box.Closing += Box_Closing;
			_box.WindowStyle = WindowStyle.ToolWindow;

			
			TextBlock content = new TextBlock();
			content.TextWrapping = TextWrapping.Wrap;
			content.Background = null;
			content.HorizontalAlignment = HorizontalAlignment.Center;
			content.FontSize = _fontSize;
			_sp1.Children.Add(content);

			_input.FontSize = _fontSize;
			_input.HorizontalAlignment = HorizontalAlignment.Center;
			_input.MinWidth = 200;
			_input.MouseEnter += input_MouseDown;
			_sp1.Children.Add(_input);


			_sp2.Orientation = Orientation.Horizontal;
			_sp2.HorizontalAlignment = HorizontalAlignment.Center;
			_sp2.Margin = new Thickness(0, 10, 0, 0);

			_ok.Width = 70;
			_ok.Height = 30;
			_ok.Click += ok_Click;
			_ok.Content = _okbuttontext;
			_ok.HorizontalAlignment = HorizontalAlignment.Center;
			_sp2.Children.Add(_ok);

			_cancel.Width = 70;
			_cancel.Height = 30;
			_cancel.Margin = new Thickness(10, 0, 0, 0);
			_cancel.Click += cancel_Click;
			_cancel.Content = "Cancel";
			_cancel.HorizontalAlignment = HorizontalAlignment.Center;
			_sp2.Children.Add(_cancel);


			_sp1.Children.Add(_sp2);
		}

		void Box_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!_clicked)
				e.Cancel = true;
		}

		private void input_MouseDown(object sender, MouseEventArgs e)
		{
			if ((sender as TextBox).Text == _defaulttext && _inputreset == false)
			{
				(sender as TextBox).Text = null;
				_inputreset = true;
			}
		}

		void ok_Click(object sender, RoutedEventArgs e)
		{
			_clicked = true;

				_box.Close();
			_clicked = false;
		}

		void cancel_Click(object sender, RoutedEventArgs e)
		{
			_clicked = true;
			_input.Text = "";
			_box.Close();
			_clicked = false;
		}

		public string ShowDialog()
		{
			_box.ShowDialog();
			return _input.Text;
		}
	}
}
