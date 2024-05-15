using ScottPlot;
namespace Embedded_Systems_Project
{
    internal class LiveGraphUpdater
    {
        private readonly Plot _plot;
        private readonly double[] _temperatures, _tempNew;
        private readonly int _maxDataPoints = 100; // maximum number of data points to display

        public LiveGraphUpdater(ScottPlot.WinForms.FormsPlot plot)
        {
            _plot = plot.Plot;

            // Initialize arrays to store data
            _temperatures = new double[_maxDataPoints];
            _temperatures = new double[_maxDataPoints];
        }

        public void StartUpdating(double[] initialTemperatures)
        {
            // Add initial data
            Array.Copy(initialTemperatures, _temperatures, Math.Min(initialTemperatures.Length, _maxDataPoints));
            _plot.Clear();
            _ = _plot.Add.Signal(_temperatures);
        }

        public void Update(double currentTemperature)
        {
            // Shift data to make room for new point
            Array.Copy(_temperatures, 1, _temperatures, 0, _maxDataPoints - 1);

            // Add new data point
            _temperatures[_maxDataPoints - 1] = currentTemperature;

            // Update the plot with new data
            _plot.Axes.AutoScaleX();
            _plot.Axes.AutoScaleY();
            _plot.Clear();
            _ = _plot.Add.Signal(_temperatures);
        }
    }
}
