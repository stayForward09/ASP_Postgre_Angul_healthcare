import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import {SalesDataService} from '../../services/sales-data.service';
import * as moment from 'moment';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {

  constructor(private _salesDataService: SalesDataService) { }

  orders: any;
  orderLabels !: string[];
  orderData !: number[];

  public barChartData !: any[];
  public barChartLabels !: string[];
  public barChartType : ChartType = 'bar';
  public barChartLegend = true;
  public barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  ngOnInit() {
    this._salesDataService.getOrders(1, 100)
      .subscribe(res => {
        // console.log(res['page']['data']);
        const localChartData = this.getChartData(res);
        this.barChartLabels = localChartData.map(x => x[0]).reverse();
        this.barChartData = [{ 'data': localChartData.map(x => x[1]), 'label': 'Sales'}];
      });
  }

  getChartData(res: Response) {
    this.orders = res['page']['data'];
    const data = this.orders.map(o => o.total);

    const formattedOrders = this.orders.reduce((r, e) => {
      r.push([moment(e.placed).format('YY-MM-DD'), e.total]);
      return r;
    }, []);

    const p : any[]= [];

    const chartData = formattedOrders.reduce((r, e) => {
      const key = e[0];
      if (!p[key]) {
        p[key] = e;
        r.push(p[key]);
      } else {
        p[key][1] += e[1];
      }
      return r;
    }, []);

    return chartData;

  }
}