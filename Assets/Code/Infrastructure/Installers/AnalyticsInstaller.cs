using System.Collections.Generic;
using Code.Analytics;
using Code.Analytics.AnalyticsAdapters;
using Code.Analytics.GoogleSheetsIntegration;
using Code.Extensions;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class AnalyticsInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			IEnumerable<IAnalytic> analytics = new List<IAnalytic>
			{
				new Analytics1Adapter(),
				new Analytics2Adapter(),
			};

			Container
				.BindSingleFromInstance(analytics)
				// .BindSingleWithInterfaces<AnalyticsMock>()
				.BindSingleWithInterfaces<CvsLoadMock>()
				.BindSingleWithInterfaces<GoogleSheetLoader>()
				.BindSingle<CvsLoader>()
				.BindSingle<SheetProcessor>()
				;
		}
	}
}