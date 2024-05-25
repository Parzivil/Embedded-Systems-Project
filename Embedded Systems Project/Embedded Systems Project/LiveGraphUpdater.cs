
using System.Windows.Forms.DataVisualization.Charting;

namespace Embedded_Systems_Project
{
    internal class LiveGraphUpdater
    {
        System.Windows.Forms.DataVisualization.Charting.Chart plot;

        Series seriesMain;
        Series seriesSecond;

        public int maxWidth = 500;

        long count = 0;

        ChartArea area;

        public LiveGraphUpdater(System.Windows.Forms.DataVisualization.Charting.Chart _plot)
        {
            plot = _plot;

            area = plot.ChartAreas[0];

            seriesMain = plot.Series.FindByName("Temp");
            seriesSecond = plot.Series.FindByName("Target");

        }

        public void Update(double temp, double target)
        {
            if (count > maxWidth)
            {
                seriesMain.Points.RemoveAt(0);
                seriesSecond.Points.RemoveAt(0);

                area.RecalculateAxesScale();
            }


            seriesMain.Points.AddXY(count, temp);
            seriesSecond.Points.AddXY(count, target);
            count++;

            plot.Update();

        }
    }
}
