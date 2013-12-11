﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeatMapSeriesExamples.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2012 Oystein Bjorke
//   
//   Permission is hereby granted, free of charge, to any person obtaining a
//   copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//   
//   The above copyright notice and this permission notice shall be included
//   in all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//   OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//   IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//   CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//   SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleLibrary
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [Examples("HeatMapSeries")]
    public class HeatMapSeriesExamples : ExamplesBase
    {
        [Example("Peaks")]
        public static PlotModel Peaks()
        {
            return CreatePeaks();
        }

        public static PlotModel CreatePeaks(OxyPalette palette = null, bool includeContours = true)
        {
            double x0 = -3.1;
            double x1 = 3.1;
            double y0 = -3;
            double y1 = 3;
            Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1)) - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y) - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
            var xvalues = ArrayHelper.CreateVector(x0, x1, 100);
            var yvalues = ArrayHelper.CreateVector(y0, y1, 100);
            var peaksData = ArrayHelper.Evaluate(peaks, xvalues, yvalues);

            var model = new PlotModel("Peaks");
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = palette ?? OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            var hms = new HeatMapSeries { X0 = x0, X1 = x1, Y0 = y0, Y1 = y1, Data = peaksData };
            model.Series.Add(hms);
            if (includeContours)
            {
                var cs = new ContourSeries
                             {
                                 Color = OxyColors.Black,
                                 FontSize = 0,
                                 ContourLevelStep = 1,
                                 LabelBackground = OxyColors.Undefined,
                                 ColumnCoordinates = yvalues,
                                 RowCoordinates = xvalues,
                                 Data = peaksData
                             };
                model.Series.Add(cs);
            }

            return model;
        }

        [Example("Interpolated")]
        public static PlotModel Interpolated()
        {
            return CreateExample("Interpolated 2x3", true);
        }

        [Example("Interpolated on Cartesian axes")]
        public static PlotModel InterpolatedCartesian()
        {
            var model = CreateExample("Interpolated 2x3", true);
            model.PlotType = PlotType.Cartesian;
            return model;
        }

        [Example("Interpolated with two NaN values")]
        public static PlotModel InterpolatedWithNanValue()
        {
            var model = CreateExample("Interpolated including two NaN values", true);
            var hms = (HeatMapSeries)model.Series[0];
            hms.Data[0, 1] = double.NaN;
            hms.Data[1, 0] = double.NaN;
            return model;
        }

        [Example("Not interpolated")]
        public static PlotModel NotInterpolated()
        {
            return CreateExample("Not interpolated values", false);
        }

        [Example("Not interpolated, reversed x-axis")]
        public static PlotModel NotInterpolatedReversedX()
        {
            var model = CreateExample("Not interpolated values", false);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, StartPosition = 1, EndPosition = 0 });
            return model;
        }

        [Example("Not interpolated, reversed y-axis")]
        public static PlotModel NotInterpolatedReversedY()
        {
            var model = CreateExample("Not interpolated values", false);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, StartPosition = 1, EndPosition = 0 });
            return model;
        }

        [Example("Not interpolated, reversed x- and y-axis")]
        public static PlotModel NotInterpolatedReversedXY()
        {
            var model = CreateExample("Not interpolated values", false);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, StartPosition = 1, EndPosition = 0 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, StartPosition = 1, EndPosition = 0 });
            return model;
        }

        [Example("Not interpolated with two NaN values")]
        public static PlotModel NotInterpolatedWithNanValue()
        {
            var model = CreateExample("Not interpolated values including two NaN values", false);
            var ca = (LinearColorAxis)model.Axes[0];
            ca.InvalidNumberColor = OxyColors.Transparent;
            var hms = (HeatMapSeries)model.Series[0];
            hms.Data[0, 1] = double.NaN;
            hms.Data[1, 0] = double.NaN;
            return model;
        }

        [Example("Diagonal (center defined)")]
        public static PlotModel Diagonal()
        {
            var data = new double[3, 3];
            data[0, 0] = 1;
            data[1, 1] = 1;
            data[2, 2] = 1;

            var model = new PlotModel("Diagonal");
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            // adding half cellwidth/cellheight to bounding box coordinates
            var hms = new HeatMapSeries { CoordinateDefinition = HeatMapCoordinateDefinition.Center, X0 = 0.5, X1 = 2.5, Y0 = 2.5, Y1 = 0.5, Data = data, Interpolate = false };
            model.Series.Add(hms);
            return model;
        }

        [Example("Diagonal (edge defined)")]
        public static PlotModel Diagonal2()
        {
            var data = new double[3, 3];
            data[0, 0] = 1;
            data[1, 1] = 1;
            data[2, 2] = 1;

            var model = new PlotModel("Diagonal");
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            // adding half cellwidth/cellheight to bounding box coordinates
            var hms = new HeatMapSeries { CoordinateDefinition = HeatMapCoordinateDefinition.Edge, X0 = 0, X1 = 3, Y0 = 3, Y1 = 0, Data = data, Interpolate = false };
            model.Series.Add(hms);
            return model;
        }

        [Example("Diagonal (6x6)")]
        public static PlotModel Diagonal_6X6()
        {
            // https://oxyplot.codeplex.com/discussions/453174
            var data = new double[6, 6];
            data[0, 0] = 1;
            data[1, 1] = 1;
            data[2, 2] = 1;
            data[3, 3] = 1;
            data[4, 4] = 1;
            data[5, 5] = 1;

            var model = new PlotModel("Diagonal");
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            // note: the coordinates are specifying the centers of the edge cells
            var hms = new HeatMapSeries { X0 = 0, X1 = 5, Y0 = 0, Y1 = 5, Data = data, Interpolate = true };
            model.Series.Add(hms);
            return model;
        }

        /// <summary>
        /// Creates a simple 2x3 example heat map.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="interpolate">Interpolate the HeatMapSeries if set to <c>true</c>.</param>
        /// <returns>A <see cref="PlotModel"/>.</returns>
        private static PlotModel CreateExample(string title, bool interpolate)
        {
            var data = new double[2, 3];
            data[0, 0] = 0;
            data[0, 1] = 0.2;
            data[0, 2] = 0.4;
            data[1, 0] = 0.1;
            data[1, 1] = 0.3;
            data[1, 2] = 0.2;

            var model = new PlotModel(title, "Bounding box should be [0,2] and [0,3]");
            model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            // adding half cellwidth/cellheight to bounding box coordinates
            var hms = new HeatMapSeries
            {
                CoordinateDefinition = HeatMapCoordinateDefinition.Center,
                X0 = 0.5,
                X1 = 1.5,
                Y0 = 0.5,
                Y1 = 2.5,
                Data = data,
                Interpolate = interpolate,
                LabelFontSize = 0.2
            };
            model.Series.Add(hms);
            return model;
        }

        [Example("Confusion matrix")]
        public static PlotModel ConfusionMatrix()
        {
            // Example provided by Pau Climent Pérez
            // See also http://en.wikipedia.org/wiki/Confusion_matrix
            var data = new double[3, 3];

            data[0, 0] = 1;
            data[1, 1] = 0.8;
            data[1, 2] = 0.2;
            data[2, 2] = 1;

            // I guess this is where the confusion comes from?
            data = data.Transpose();

            string[] cat1 = { "class A", "class B", "class C" };

            var model = new PlotModel("Confusion Matrix");

            var palette = OxyPalette.Interpolate(50, OxyColors.White, OxyColors.Black);

            var lca = new LinearColorAxis { Position = AxisPosition.Right, Palette = palette, HighColor = OxyColors.White, LowColor = OxyColors.White };
            model.Axes.Add(lca);

            var axis1 = new CategoryAxis(AxisPosition.Top, "Actual class", cat1);
            model.Axes.Add(axis1);

            // We invert this axis, so that they look "symmetrical"
            var axis2 = new CategoryAxis(AxisPosition.Left, "Predicted class", cat1);
            axis2.Angle = -90;
            axis2.StartPosition = 1;
            axis2.EndPosition = 0;

            model.Axes.Add(axis2);

            var hms = new HeatMapSeries
            {
                Data = data,
                Interpolate = false,
                LabelFontSize = 0.25,
                X0 = 0,
                X1 = data.GetLength(1) - 1,
                Y0 = 0,
                Y1 = data.GetLength(0) - 1,
            };

            model.Series.Add(hms);
            return model;
        }
    }
    
    internal static class ArrayExtensions
    {
        public static double[,] Transpose(this double[,] input)
        {
            int m = input.GetLength(0);
            int n = input.GetLength(1);
            var output = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    output[j, i] = input[i, j];
                }
            }

            return output;
        }
    }
}