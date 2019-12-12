using System;
using System.Windows.Forms;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class GUIExceptionsHandler: IExceptionHandler
	{
		public void Handle(Exception exception)
		{
			var caption = "Something went wrong";
			if (exception is ArgumentException)
				caption = "Wrong input";
			MessageBox.Show(exception.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}