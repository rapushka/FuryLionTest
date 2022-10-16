using System;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public interface ICvsLoader
	{
		public void LoadTable(Action<string> onSheetLoaded);
	}
}