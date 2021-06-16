import { Component, OnInit } from '@angular/core';
import {Order} from './../../shared/order'
import { SalesDataService } from '../../services/sales-data.service';

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})


// class 
export class SectionOrdersComponent implements OnInit {

  constructor(private _salesData: SalesDataService) { }



  orders !: Order[];
  total = 0;
  page = 1;
  limit = 10;
  loading = false;


  // Init
  ngOnInit(): void {
    this.getOrders();
  }//end init


  // get Orders
  getOrders(): void {

    this._salesData.getOrders(this.page, this.limit)
      .subscribe(res => {
        console.log('Result from getOrders: ', res);
        this.orders = res['page']['data'];
        this.total = res['page'].total;
        this.loading = false
      });
  }//end getOrders


// go to PreviousPage
  goToPrevious(): void {
    this.page--;
    this.getOrders();
  }//end goToPrevious


  // go to NextPage
  goToNext(): void {
    this.page++;
    this.getOrders();
  }//end goToNext


  // go to Page number = ?
  goToPage(n: number): void{
    this.page = n;
    this.getOrders();
  }//end goToPage


}//end class
