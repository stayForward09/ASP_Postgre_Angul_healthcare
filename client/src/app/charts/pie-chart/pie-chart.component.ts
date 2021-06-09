import { Component, OnInit } from '@angular/core';

import {ChartType} from 'chart.js';
import { SingleDataSet } from 'ng2-charts';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  constructor() { }

  pieChartData: SingleDataSet = [350, 450, 120];
  pieChartLabels: string[] = ['A', 'B', 'C'];
  pieCharttype: ChartType = 'pie';
  colors: any[] = [
    {
      backgroundColor: ['#26547c', '#ff6b6b', '#ffd166']
    }
  ];

  pieChartType: ChartType= 'pie';


  ngOnInit(): void {
  }

}
