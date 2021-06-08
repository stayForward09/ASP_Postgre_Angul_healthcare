import { Component, OnInit } from '@angular/core';

import {ChartType} from 'chart.js';

const LINE_CHART_SAMPLE_DATA: any[] = [
  { data: [32, 14, 46, 23, 38, 56], label: 'D'},
  { data: [12, 18, 26, 13, 28, 26], label: 'E'},
  { data: [52, 34, 49, 53, 68, 62], label: 'F'},
];

const LINE_CHART_LABELS: string[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {

  constructor() { }

  lineChartData = LINE_CHART_SAMPLE_DATA;
  lineChartLabels = LINE_CHART_LABELS;
  lineChartLegend = false;
  lineChartOptions: any = {
    responsive: true
  };

  
  lineChartType: ChartType = 'line'

  ngOnInit(): void {
  }

}
