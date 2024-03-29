﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using travelAgency.model;
using travelAgency.view;

namespace travelAgency.components
{
    /// <summary>
    /// Interaction logic for ReportCard.xaml
    /// </summary>
    public partial class ReportCard : UserControl
    {
        public Trip Trip {
            get { return (Trip)GetValue(TripProperty); }
            set
            {
                SetValue(TripProperty, value);
            }
        }
        public double TotalPrice { get; set; }
        public int TotalCount { get; set; }
        public ReportCard()
        {
            InitializeComponent();
            DataContext = this;
        }
        public static readonly DependencyProperty TripProperty = DependencyProperty.Register("Trip", typeof(Trip), typeof(ReportCard), new PropertyMetadata(null));
    }
}