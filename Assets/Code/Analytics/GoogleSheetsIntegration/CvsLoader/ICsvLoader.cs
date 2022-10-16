using System;

namespace Code.Analytics.GoogleSheetsIntegration.CvsLoader
{
	public interface ICsvLoader
	{
		void LoadTable(Action<string> onSheetLoaded);
	}
}