using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotModel = OxyPlot.PlotModel;
using IDataPoint = OxyPlot.IDataPoint;
using DataPoint = OxyPlot.DataPoint;
using OxyColor = OxyPlot.OxyColor;
using OxyColors = OxyPlot.OxyColors;

namespace ComputationalPhysics {
    public class Charting {
        public static OxyPlot.PlotModel Chart() {
            var points = (List<OxyPlot.IDataPoint>)Enumerable.Range(0, 50).Select(i => (OxyPlot.IDataPoint)(new OxyPlot.DataPoint(i, rand.Next()))).ToList();
            return lineChart(points);
        }

        public static PlotModel Chart(Func<int, string> domain, Func<int, double> range, int start, int end) {
            Func<int, DateTime> dateGen2 = i => DateTime.Parse(domain(i));
            return Chart(dateGen2, range, start, end);
        }

        public static PlotModel Chart(Func<int, TimeSpan> domain, Func<int, double> range, int start, int end) {
            var points = (List<IDataPoint>)Enumerable.Range(start, end).Select(i => (IDataPoint)(
                new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now.Date + domain(i)), range(i))))
                .ToList();
            return lineChart(points, true);
        }

        public static PlotModel Chart(Func<int, DateTime> domain, Func<int, double> range, int start, int end) {
            var points = (List<IDataPoint>)Enumerable.Range(start, end).Select(i => (IDataPoint)(
                new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(domain(i)), range(i))))
                .ToList();
            return lineChart(points, true);
        }

        public static PlotModel Chart(Func<int, DateTime> domain, int start, int end, params Func<int, double>[] range) {
            List<IDataPoint>[] points = new List<IDataPoint>[range.Count()];
            for(int idx = 0; idx < range.Count(); idx++){
                var r = range[idx];
                var p = (List<IDataPoint>)Enumerable.Range(start, end).Select(i => (IDataPoint)(
                    new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(domain(i)), r(i))))
                    .ToList();
                points[idx] = (p);
            }
            return lineChart(true, points);
        }

        public static PlotModel Chart(Func<int, TimeSpan> domain, int start, int end, params Func<int, double>[] range) {
            List<IDataPoint>[] points = new List<IDataPoint>[range.Count()];
            for (int idx = 0; idx < range.Count(); idx++) {
                var r = range[idx];
                var p = (List<IDataPoint>)Enumerable.Range(start, end).Select(i => (IDataPoint)(
                    new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(DateTime.Now + domain(i)), r(i))))
                    .ToList();
                points[idx] = (p);
            }
            return lineChart(true, points);
        }

        public static PlotModel Chart(Func<int, double> range, int start, int end) {
            var points = (List<IDataPoint>)Enumerable.Range(start, end).Select(i => (IDataPoint)(new DataPoint(i, range(i)))).ToList();
            return lineChart(points);
        }

        public static PlotModel Chart(List<double> domain, List<double> range, bool timeDomain) {
            if (domain.Count() != range.Count()) {
                throw new Exception("Domain and range must have the same amount of elements");
            }
            List<DataPoint> pts = new List<DataPoint>();
            for (int i = 0; i < domain.Count(); i++) {
                pts.Add(new DataPoint(domain[i], range[i]));
            }
            List<IDataPoint> pt2 = pts.Cast<IDataPoint>().ToList();
            return lineChart(pt2, timeDomain);
        }

        public static PlotModel Chart(List<double> domain, 
            bool isTimeDomain,
            IEnumerable<List<double>> ranges) {
            List<IDataPoint>[] lines = new List<IDataPoint>[ranges.Count()];
            int count =0 ;
                foreach (var r in ranges) {
                    if (domain.Count() != r.Count()) {
                        throw new Exception("Domain and range must have the same amount of elements");
                    }
                    List<DataPoint> pts = new List<DataPoint>();
                    for (int i = 0; i < domain.Count(); i++) {
                        pts.Add(new DataPoint(domain[i], r[i]));
                    }
                    lines[count++] = pts.Cast<IDataPoint>().ToList();
                }
                return lineChart(isTimeDomain, lines);

        }

        public static PlotModel Chart(List<double> domain, 
            List<double> range1,
            List<double> range2, 
            bool timeDomain) {
            if (domain.Count() != range1.Count() ||
                domain.Count() != range2.Count()) {
                throw new Exception("Domain and range must have the same amount of elements");
            }
            List<DataPoint> pts = new List<DataPoint>();
            for (int i = 0; i < domain.Count(); i++) {
                pts.Add(new DataPoint(domain[i], range1[i]));
            }
            List<IDataPoint> pts1 = pts.Cast<IDataPoint>().ToList();


            pts = new List<DataPoint>();
            for (int i = 0; i < domain.Count(); i++) {
                pts.Add(new DataPoint(domain[i], range2[i]));
            }
            List<IDataPoint> pts2 = pts.Cast<IDataPoint>().ToList();
            return lineChart(timeDomain, pts1, pts2);
        }

        public static PlotModel Chart(IEnumerable<double> data) {
            var points = (List<IDataPoint>)data.Select((i, j) => (IDataPoint)(new DataPoint(j, i))).ToList();
            return lineChart(points);
        }

        private static OxyPlot.PlotModel lineChart(bool dateTimeAxis, params List<OxyPlot.IDataPoint>[] points) {
            var model = new OxyPlot.PlotModel();
            if (dateTimeAxis) {
                model.Axes.Add(new OxyPlot.Axes.DateTimeAxis());
            }
            List<OxyColor> colors = new List<OxyColor> { OxyColors.Blue, OxyColors.Red, OxyColors.Green, OxyColors.Purple, OxyColors.Black };
            int counter = 0;
            foreach (var p in points) {
                var lineSeries = new OxyPlot.Series.LineSeries();
                lineSeries.CanTrackerInterpolatePoints = false;
                lineSeries.StrokeThickness = 1;

                lineSeries.Color = colors[counter++];
                lineSeries.Points = p;
                model.Series.Add(lineSeries);
            }
            return model;
        }

        private static OxyPlot.PlotModel lineChart(List<OxyPlot.IDataPoint> points, bool dateTimeAxis = false) {
            return lineChart(dateTimeAxis, points);
        }

        private static Random rand = new Random();
    }
}
