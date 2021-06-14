import { Component, OnInit, Input} from '@angular/core';

import {ChartType} from 'chart.js';


@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {

  constructor() { }

  @Input() inputData: any;
  @Input() limit !: number;



  pieChartData: number[];
  pieChartLabels: string[];
  colors: any[] = [
    {
      backgroundColor: ['#26547c', '#ff6b6b', '#ffd166'],
      borderColor: '#111'
    }
  ];
  pieChartType: ChartType= 'doughnut';


  ngOnInit(){
    this.parseChartData(this.inputData, this.limit);
  }

  parseChartData(res: any, limit?: number) {
    const allData = res.slice(0, limit);
    this.pieChartData = allData.map(x => x[Object.keys(x)[1]]);
    this.pieChartLabels = allData.map(x => x[Object.keys(x)[0]]);
  }

}