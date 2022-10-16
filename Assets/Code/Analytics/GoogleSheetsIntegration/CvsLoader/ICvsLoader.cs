using System;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public interface ICvsLoader
	{
		void LoadTable(Action<string> onSheetLoaded);
	}
}