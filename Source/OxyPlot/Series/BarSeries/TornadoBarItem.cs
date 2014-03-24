﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TornadoBarItem.cs" company="OxyPlot">
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
// <summary>
//   Represents an item for the TornadoBarSeries.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Series
{
    /// <summary>
    /// Represents an item for the TornadoBarSeries.
    /// </summary>
    public class TornadoBarItem : CategorizedItem, ICodeGenerating
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TornadoBarItem"/> class.
        /// </summary>
        public TornadoBarItem()
        {
            this.Minimum = double.NaN;
            this.Maximum = double.NaN;
            this.BaseValue = double.NaN;
            this.MinimumColor = OxyColors.Automatic;
            this.MaximumColor = OxyColors.Automatic;
        }

        /// <summary>
        /// Gets or sets the base value.
        /// </summary>
        public double BaseValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the color for the maximum bar.
        /// </summary>
        public OxyColor MaximumColor { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// Gets or sets the color for the minimum bar.
        /// </summary>
        public OxyColor MinimumColor { get; set; }

        /// <summary>
        /// Returns c# code that generates this instance.
        /// </summary>
        /// <returns>
        /// C# code.
        /// </returns>
        public string ToCode()
        {
            if (!this.MaximumColor.IsUndefined())
            {
                return CodeGenerator.FormatConstructor(
                    this.GetType(),
                    "{0},{1},{2},{3},{4}",
                    this.Minimum,
                    this.Maximum,
                    this.BaseValue,
                    this.MinimumColor.ToCode(),
                    this.MaximumColor.ToCode());
            }

            if (!this.MinimumColor.IsUndefined())
            {
                return CodeGenerator.FormatConstructor(
                    this.GetType(),
                    "{0},{1},{2},{3}",
                    this.Minimum,
                    this.Maximum,
                    this.BaseValue,
                    this.MinimumColor.ToCode());
            }

            if (!double.IsNaN(this.BaseValue))
            {
                return CodeGenerator.FormatConstructor(
                    this.GetType(), "{0},{1},{2}", this.Minimum, this.Maximum, this.BaseValue);
            }

            return CodeGenerator.FormatConstructor(this.GetType(), "{0},{1}", this.Minimum, this.Maximum);
        }
    }
}