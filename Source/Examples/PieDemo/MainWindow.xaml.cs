﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;

namespace PieDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel PieModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var tmp = new PlotModel();
            tmp.Title = "World population by continent";
            // http://www.nationsonline.org/oneworld/world_population.htm
            // http://en.wikipedia.org/wiki/Continent

            var ps = new PieSeries();
            ps.Slices.Add(new PieSlice("Africa", 1030, OxyColors.Brown) { IsExploded = true });
            ps.Slices.Add(new PieSlice("Americas", 929, OxyColors.Red) { IsExploded = true });
            ps.Slices.Add(new PieSlice("Asia", 4157, OxyColors.Yellow));
            ps.Slices.Add(new PieSlice("Europe", 739, OxyColors.Green) { IsExploded = true });
            ps.Slices.Add(new PieSlice("Oceania", 35, OxyColors.Blue) { IsExploded = true });
            ps.InnerDiameter = 0.2;
            ps.ExplodedDistance = 0;
            ps.Stroke = OxyColors.Black;
            ps.StrokeThickness = 1.0;
            ps.AngleSpan = 360;
            ps.StartAngle = 0;
            tmp.Series.Add(ps);
            
            PieModel = tmp;
            DataContext = this;
        }
    }
}
