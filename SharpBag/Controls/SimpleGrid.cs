using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SharpBag
{
    /// <summary>
    /// A simple grid.
    /// </summary>
    public class SimpleGrid : Grid
    {
        /// <summary>
        /// Row property.
        /// </summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(string), typeof(SimpleGrid), new UIPropertyMetadata(null, new PropertyChangedCallback(RowsChanged)));

        /// <summary>
        /// Column property.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(string), typeof(SimpleGrid), new UIPropertyMetadata(null, new PropertyChangedCallback(ColumnsChanged)));

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public string Columns
        {
            get { return (string)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        private static IEnumerable<GridLength> GridLengths(string s)
        {
            GridLengthConverter converter = new GridLengthConverter();
            return s.Split(',').Select(i => converter.ConvertFromString(i)).OfType<GridLength>();
        }

        private static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SimpleGrid grid = d as SimpleGrid;
            grid.RowDefinitions.Clear();
            foreach (GridLength length in GridLengths(grid.Rows)) grid.RowDefinitions.Add(new RowDefinition { Height = length });
        }

        private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SimpleGrid grid = d as SimpleGrid;
            grid.ColumnDefinitions.Clear();
            foreach (GridLength length in GridLengths(grid.Columns)) grid.ColumnDefinitions.Add(new ColumnDefinition { Width = length });
        }
    }
}