﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderContext.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// <summary>
//   Horizontal text alignment.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    using System.Collections.Generic;

    /// <summary>
    /// Horizontal text alignment.
    /// </summary>
    public enum HorizontalTextAlign
    {
        /// <summary>
        /// Left alignment.
        /// </summary>
        Left = -1, 

        /// <summary>
        /// Center alignment.
        /// </summary>
        Center = 0, 

        /// <summary>
        /// Right alignment.
        /// </summary>
        Right = 1
    }

    /// <summary>
    /// Vertical text alignment.
    /// </summary>
    public enum VerticalTextAlign
    {
        /// <summary>
        /// Top alignment.
        /// </summary>
        Top = -1, 

        /// <summary>
        /// Middle alignment.
        /// </summary>
        Middle = 0, 

        /// <summary>
        /// Bottom alignment.
        /// </summary>
        Bottom = 1
    }

    /// <summary>
    /// Pen line join.
    /// </summary>
    public enum OxyPenLineJoin
    {
        /// <summary>
        /// Line joins use regular angular vertices.
        /// </summary>
        Miter, 

        /// <summary>
        /// Line joins use rounded vertices.
        /// </summary>
        Round, 

        /// <summary>
        /// Line joins use beveled vertices.
        /// </summary>
        Bevel
    }

    /// <summary>
    /// Render context interface.
    /// </summary>
    public interface IRenderContext
    {
        #region Public Properties

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        double Height { get; }

        /// <summary>
        /// Gets a value indicating whether to paint the background.
        /// </summary>
        /// <value><c>true</c> if the background should be painted; otherwise, <c>false</c>.</value>
        bool PaintBackground { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        double Width { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="rect">
        /// The rectangle.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        void DrawEllipse(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness = 1.0);

        /// <summary>
        /// Draws the collection of ellipses, where all have the same stroke and fill.
        /// This performs better than calling DrawEllipse multiple times.
        /// </summary>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        void DrawEllipses(IList<OxyRect> rectangles, OxyColor fill, OxyColor stroke, double thickness = 1.0);

        /// <summary>
        /// Draws the polyline from the specified points.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join type.
        /// </param>
        /// <param name="aliased">
        /// if set to <c>true</c> the shape will be aliased.
        /// </param>
        void DrawLine(
            IList<ScreenPoint> points, 
            OxyColor stroke, 
            double thickness = 1.0, 
            double[] dashArray = null, 
            OxyPenLineJoin lineJoin = OxyPenLineJoin.Miter, 
            bool aliased = false);

        /// <summary>
        /// Draws the multiple line segments defined by points (0,1) (2,3) (4,5) etc.
        /// This should have better performance than calling DrawLine for each segment.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join type.
        /// </param>
        /// <param name="aliased">
        /// if set to <c>true</c> the shape will be aliased.
        /// </param>
        void DrawLineSegments(
            IList<ScreenPoint> points, 
            OxyColor stroke, 
            double thickness = 1.0, 
            double[] dashArray = null, 
            OxyPenLineJoin lineJoin = OxyPenLineJoin.Miter, 
            bool aliased = false);

        /// <summary>
        /// Draws the polygon from the specified points. The polygon can have stroke and/or fill.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join type.
        /// </param>
        /// <param name="aliased">
        /// if set to <c>true</c> the shape will be aliased.
        /// </param>
        void DrawPolygon(
            IList<ScreenPoint> points, 
            OxyColor fill, 
            OxyColor stroke, 
            double thickness = 1.0, 
            double[] dashArray = null, 
            OxyPenLineJoin lineJoin = OxyPenLineJoin.Miter, 
            bool aliased = false);

        /// <summary>
        /// Draws a collection of polygons, where all polygons have the same stroke and fill.
        /// This performs better than calling DrawPolygon multiple times.
        /// </summary>
        /// <param name="polygons">
        /// The polygons.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join type.
        /// </param>
        /// <param name="aliased">
        /// if set to <c>true</c> the shape will be aliased.
        /// </param>
        void DrawPolygons(
            IList<IList<ScreenPoint>> polygons, 
            OxyColor fill, 
            OxyColor stroke, 
            double thickness = 1.0, 
            double[] dashArray = null, 
            OxyPenLineJoin lineJoin = OxyPenLineJoin.Miter, 
            bool aliased = false);

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rectangle.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        void DrawRectangle(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness = 1.0);

        /// <summary>
        /// Draws a collection of rectangles, where all have the same stroke and fill.
        /// This performs better than calling DrawRectangle multiple times.
        /// </summary>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="stroke">
        /// The stroke color.
        /// </param>
        /// <param name="thickness">
        /// The stroke thickness.
        /// </param>
        void DrawRectangles(IList<OxyRect> rectangles, OxyColor fill, OxyColor stroke, double thickness = 1.0);

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fill">
        /// The fill color.
        /// </param>
        /// <param name="fontFamily">
        /// The font family.
        /// </param>
        /// <param name="fontSize">
        /// Size of the font.
        /// </param>
        /// <param name="fontWeight">
        /// The font weight.
        /// </param>
        /// <param name="rotate">
        /// The rotatation angle.
        /// </param>
        /// <param name="halign">
        /// The horizontal alignment.
        /// </param>
        /// <param name="valign">
        /// The vertical alignment.
        /// </param>
        void DrawText(
            ScreenPoint p, 
            string text, 
            OxyColor fill, 
            string fontFamily = null, 
            double fontSize = 10, 
            double fontWeight = 500, 
            double rotate = 0, 
            HorizontalTextAlign halign = HorizontalTextAlign.Left, 
            VerticalTextAlign valign = VerticalTextAlign.Top);

        /// <summary>
        /// Measures the text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fontFamily">
        /// The font family.
        /// </param>
        /// <param name="fontSize">
        /// Size of the font.
        /// </param>
        /// <param name="fontWeight">
        /// The font weight.
        /// </param>
        /// <returns>
        /// The text size.
        /// </returns>
        OxySize MeasureText(string text, string fontFamily = null, double fontSize = 10, double fontWeight = 500);

        #endregion
    }
}