
using System.Windows.Forms.DataVisualization.Charting;

namespace Embedded_Systems_Project
{
    internal class LiveGraphUpdater
    {
        System.Windows.Forms.DataVisualization.Charting.Chart plot;

        Series seriesMain;
        Series seriesSecond;
 
        public int maxWidth = 10000;

        long count = 0;



        public LiveGraphUpdater(System.Windows.Forms.DataVisualization.Charting.Chart _plot)
        {
            plot = _plot;

            seriesMain = plot.Series.FindByName("Temp");
            seriesSecond = plot.Series.FindByName("Target");

        }
        
        public void Update(double temp, double target) {
                seriesMain.Points.AddXY(count, temp);
                seriesSecond.Points.AddXY(count, target);
                count++;

                if (count > maxWidth)
                {
                    count = 0;
                    seriesMain.Points.Clear();
                    seriesSecond.Points.Clear();
                }

                plot.Update();
            
        }
    }
}
