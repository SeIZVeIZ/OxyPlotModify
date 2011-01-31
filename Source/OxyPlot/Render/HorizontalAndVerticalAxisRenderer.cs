﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OxyPlot
{
    /// <summary>
    ///   Rendering helper class for horizontal and vertical axes (both linear and logarithmic)
    /// </summary>
    public class HorizontalAndVerticalAxisRenderer : AxisRendererBase
    {
        public HorizontalAndVerticalAxisRenderer(IRenderContext rc, PlotModel plot)
            : base(rc, plot)
        {
        }

        public override void Render(AxisBase axis)
        {
            base.Render(axis);

            var perpendicularAxis = Plot.DefaultXAxis;
            bool isHorizontal = true;

            // Axis position (x or y screen coordinate)
            double apos = 0;

            switch (axis.Position)
            {
                case AxisPosition.Left:
                    apos = Plot.PlotArea.Left;
                    isHorizontal = false;
                    break;
                case AxisPosition.Right:
                    apos = Plot.PlotArea.Right;
                    isHorizontal = false;
                    break;
                case AxisPosition.Top:
                    apos = Plot.PlotArea.Top;
                    perpendicularAxis = Plot.DefaultYAxis;
                    break;
                case AxisPosition.Bottom:
                    apos = Plot.PlotArea.Bottom;
                    perpendicularAxis = Plot.DefaultYAxis;
                    break;
            }

            if (axis.PositionAtZeroCrossing)
            {
                apos = perpendicularAxis.Transform(0);
            }

            double a0, a1;
            var minorSegments = new List<ScreenPoint>();
            var minorTickSegments = new List<ScreenPoint>();
            var majorSegments = new List<ScreenPoint>();
            var majorTickSegments = new List<ScreenPoint>();

            if (axis.TickStyle != TickStyle.None || MinorPen != null)
            {
                GetTickPositions(axis, axis.TickStyle, axis.MinorTickSize, axis.Position, out a0, out a1);

                foreach (double value in MinorTickValues)
                {
                    if (value < axis.ActualMinimum || value > axis.ActualMaximum)
                    {
                        continue;
                    }

                    if (MajorTickValues.Contains(value))
                    {
                        continue;
                    }

                    double transformedValue = axis.Transform(value);

                    // Draw the minor grid line
                    if (MinorPen != null)
                    {
                        if (isHorizontal)
                        {
                            minorSegments.Add(new ScreenPoint(transformedValue, Plot.PlotArea.Top));
                            minorSegments.Add(new ScreenPoint(transformedValue, Plot.PlotArea.Bottom));
                        }
                        else
                        {
                            minorSegments.Add(new ScreenPoint(Plot.PlotArea.Left, transformedValue));
                            minorSegments.Add(new ScreenPoint(Plot.PlotArea.Right, transformedValue));
                        }
                    }

                    // Draw the minor tick
                    if (axis.TickStyle != TickStyle.None)
                    {
                        if (isHorizontal)
                        {
                            minorTickSegments.Add(new ScreenPoint(transformedValue, apos + a0));
                            minorTickSegments.Add(new ScreenPoint(transformedValue, apos + a1));
                        }
                        else
                        {
                            minorTickSegments.Add(new ScreenPoint(apos + a0, transformedValue));
                            minorTickSegments.Add(new ScreenPoint(apos + a1, transformedValue));
                        }
                    }
                }
            }


            if (axis.TickStyle != TickStyle.None || MajorPen != null)
            {
                GetTickPositions(axis, axis.TickStyle, axis.MajorTickSize, axis.Position, out a0, out a1);

                //double maxWidth = 0;
                //double maxHeight = 0;

                foreach (double value in MajorTickValues)
                {
                    if (value < axis.ActualMinimum || value > axis.ActualMaximum)
                    {
                        continue;
                    }

                    double transformedValue = axis.Transform(value);

                    if (MajorPen != null)
                    {
                        if (isHorizontal)
                        {
                            majorSegments.Add(new ScreenPoint(transformedValue, Plot.PlotArea.Top));
                            majorSegments.Add(new ScreenPoint(transformedValue, Plot.PlotArea.Bottom));
                        }
                        else
                        {
                            majorSegments.Add(new ScreenPoint(Plot.PlotArea.Left, transformedValue));
                            majorSegments.Add(new ScreenPoint(Plot.PlotArea.Right, transformedValue));
                        }
                    }

                    if (axis.TickStyle != TickStyle.None)
                    {
                        if (isHorizontal)
                        {
                            majorTickSegments.Add(new ScreenPoint(transformedValue, apos + a0));
                            majorTickSegments.Add(new ScreenPoint(transformedValue, apos + a1));
                        }
                        else
                        {
                            majorTickSegments.Add(new ScreenPoint(apos + a0, transformedValue));
                            majorTickSegments.Add(new ScreenPoint(apos + a1, transformedValue));
                        }
                    }

                    if (value == 0 && axis.PositionAtZeroCrossing)
                    {
                        continue;
                    }

                    var pt = new ScreenPoint();
                    var ha = HorizontalTextAlign.Right;
                    var va = VerticalTextAlign.Middle;
                    switch (axis.Position)
                    {
                        case AxisPosition.Left:
                            pt = new ScreenPoint(apos + a1 - TICK_DIST, transformedValue);
                            GetRotatedAlignments(axis.Angle, HorizontalTextAlign.Right, VerticalTextAlign.Middle, out ha,
                                                 out va);
                            break;
                        case AxisPosition.Right:
                            pt = new ScreenPoint(apos + a1 + TICK_DIST, transformedValue);
                            GetRotatedAlignments(axis.Angle, HorizontalTextAlign.Left, VerticalTextAlign.Middle, out ha,
                                                 out va);
                            break;
                        case AxisPosition.Top:
                            pt = new ScreenPoint(transformedValue, apos + a1 - TICK_DIST);
                            GetRotatedAlignments(axis.Angle, HorizontalTextAlign.Center, VerticalTextAlign.Bottom,
                                                 out ha,
                                                 out va);
                            break;
                        case AxisPosition.Bottom:
                            pt = new ScreenPoint(transformedValue, apos + a1 + TICK_DIST);
                            GetRotatedAlignments(axis.Angle, HorizontalTextAlign.Center, VerticalTextAlign.Top, out ha,
                                                 out va);
                            break;
                    }

                    string text = axis.FormatValue(value);
                    rc.DrawMathText(pt, text, Plot.TextColor,
                                    axis.FontFamily, axis.FontSize, axis.FontWeight,
                                    axis.Angle, ha, va, false);

                    // maxWidth = Math.Max(maxWidth, size.Width);
                    // maxHeight = Math.Max(maxHeight, size.Height);
                }
            }

            // Draw the zero crossing line
            if (axis.PositionAtZeroCrossing)
            {
                double t0 = axis.Transform(0);
                if (isHorizontal)
                {
                    rc.DrawLine(t0, Plot.PlotArea.Top, t0, Plot.PlotArea.Bottom, ZeroPen);
                }
                else
                {
                    rc.DrawLine(Plot.PlotArea.Left, t0, Plot.PlotArea.Right, t0, ZeroPen);
                }
            }

            // Draw extra grid lines
            if (axis.ExtraGridlines != null)
            {
                foreach (double value in axis.ExtraGridlines)
                {
                    if (!IsWithin(value, axis.ActualMinimum, axis.ActualMaximum))
                    {
                        continue;
                    }

                    double transformedValue = axis.Transform(value);
                    if (isHorizontal)
                    {
                        rc.DrawLine(transformedValue, Plot.PlotArea.Top, transformedValue, Plot.PlotArea.Bottom, ExtraPen);
                    }
                    else
                    {
                        rc.DrawLine(Plot.PlotArea.Left, transformedValue, Plot.PlotArea.Right, transformedValue, ExtraPen);
                    }
                }
            }

            // Draw the axis line (across the tick marks)
            if (isHorizontal)
            {
                majorSegments.Add(new ScreenPoint(Plot.PlotArea.Left, apos));
                majorSegments.Add(new ScreenPoint(Plot.PlotArea.Right, apos));
            }
            else
            {
                majorSegments.Add(new ScreenPoint(apos, Plot.PlotArea.Top));
                majorSegments.Add(new ScreenPoint(apos, Plot.PlotArea.Bottom));
            }

            // Draw the axis title
            if (!String.IsNullOrWhiteSpace(axis.Title))
            {
                double ymid = isHorizontal ? (axis.ScreenMin.X + axis.ScreenMax.X) / 2 : (axis.ScreenMin.Y + axis.ScreenMax.Y) / 2;

                double angle = -90;
                var lpt = new ScreenPoint();

                var halign = HorizontalTextAlign.Center;
                var valign = VerticalTextAlign.Top;

                if (axis.PositionAtZeroCrossing)
                {
                    ymid = perpendicularAxis.Transform(perpendicularAxis.ActualMaximum);
                    // valign = axis.IsReversed ? VerticalTextAlign.Top : VerticalTextAlign.Bottom;
                }

                switch (axis.Position)
                {
                    case AxisPosition.Left:
                        lpt = new ScreenPoint(AXIS_LEGEND_DIST, ymid);
                        break;
                    case AxisPosition.Right:
                        lpt = new ScreenPoint(rc.Width - AXIS_LEGEND_DIST, ymid);
                        valign = VerticalTextAlign.Bottom;
                        break;
                    case AxisPosition.Top:
                        lpt = new ScreenPoint(ymid, AXIS_LEGEND_DIST);
                        halign = HorizontalTextAlign.Center;
                        valign = VerticalTextAlign.Top;
                        angle = 0;
                        break;
                    case AxisPosition.Bottom:
                        lpt = new ScreenPoint(ymid, rc.Height - AXIS_LEGEND_DIST);
                        halign = HorizontalTextAlign.Center;
                        valign = VerticalTextAlign.Bottom;
                        angle = 0;
                        break;
                }

                rc.DrawText(lpt, axis.Title, Plot.TextColor,
                            axis.FontFamily, axis.FontSize, axis.FontWeight,
                            angle, halign, valign);
            }

            // Draw all the line segments
            rc.DrawLineSegments(minorSegments, MinorPen);
            rc.DrawLineSegments(majorSegments, MajorPen);
            rc.DrawLineSegments(minorTickSegments, MinorTickPen);
            rc.DrawLineSegments(majorTickSegments, MajorTickPen);
        }


        /// <summary>
        ///   Gets the rotated alignments given the specified angle.
        /// </summary>
        /// <param name = "angle">The angle.</param>
        /// <param name = "defaultHorizontalAlignment">The default horizontal alignment.</param>
        /// <param name = "defaultVerticalAlignment">The default vertical alignment.</param>
        /// <param name = "ha">The rotated horizontal alignment.</param>
        /// <param name = "va">The rotated vertical alignment.</param>
        private static void GetRotatedAlignments(double angle, HorizontalTextAlign defaultHorizontalAlignment,
                                                 VerticalTextAlign defaultVerticalAlignment,
                                                 out HorizontalTextAlign ha, out VerticalTextAlign va)
        {
            ha = defaultHorizontalAlignment;
            va = defaultVerticalAlignment;

            Debug.Assert(angle <= 180 && angle >= -180, "Axis angle should be in the interval [-180,180] degrees.");

            if (angle > -45 && angle < 45)
            {
                return;
            }

            if (angle > 135 || angle < -135)
            {
                ha = (HorizontalTextAlign)(-(int)defaultHorizontalAlignment);
                va = (VerticalTextAlign)(-(int)defaultVerticalAlignment);
                return;
            }

            if (angle > 45)
            {
                ha = (HorizontalTextAlign)((int)defaultVerticalAlignment);
                va = (VerticalTextAlign)(-(int)defaultHorizontalAlignment);
                return;
            }

            if (angle < -45)
            {
                ha = (HorizontalTextAlign)(-(int)defaultVerticalAlignment);
                va = (VerticalTextAlign)((int)defaultHorizontalAlignment);
                return;
            }
        }
    }
}