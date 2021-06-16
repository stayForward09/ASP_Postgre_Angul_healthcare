
import { Component, OnInit } from '@angular/core';
import { SalesDataService } from '../../services/sales-data.service';

@Component({
  selector: 'app-section-sales',
  templateUrl: './section-sales.component.html',
  styleUrls: ['./section-sales.component.css']
})


// class
export class SectionSalesComponent implements OnInit {

  salesDataByCustomer: any;
  salesDataByState: any;

  constructor(private _salesDataService: SalesDataService) { }


  // Init
  ngOnInit() {
    this._salesDataService.getOrdersByState().subscribe(res => {
      this.salesDataByState = res;
    });

    this._salesDataService.getOrdersByCustomer(5).subscribe(res => {
      this.salesDataByCustomer = res;
    });
  }//end Init


}//end class