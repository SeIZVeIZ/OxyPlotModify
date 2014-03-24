﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlotActivity.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 OxyPlot contributors
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

using Android.App;
using Android.OS;

namespace ExampleBrowser
{
    using System.Linq;

    using OxyPlot.XamarinAndroid;

    [Activity(Label = "Plot")]
    public class PlotActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var category = Intent.GetStringExtra("category");
            var plot = Intent.GetStringExtra("plot");

            var exampleInfo = ExampleLibrary.Examples.GetList().FirstOrDefault(ei => ei.Category == category && ei.Title == plot);
            var model = exampleInfo.PlotModel;
            this.Title = exampleInfo.Title;

            this.SetContentView(Resource.Layout.PlotActivity);
            var plotView = FindViewById<PlotView>(Resource.Id.plotview);
            plotView.Model = model;
        }
    }
}