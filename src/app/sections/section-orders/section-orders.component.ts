import { Component, OnInit } from '@angular/core';

import {Order} from './../../shared/order'

@Component({
  selector: 'app-section-orders',
  templateUrl: './section-orders.component.html',
  styleUrls: ['./section-orders.component.css']
})
export class SectionOrdersComponent implements OnInit {

  constructor() { }

  orders: Order[] = [
    {id: 1, customer: {id: 1, name: ' 301 Watsessing Ave', state: 'NJ', email:'xyz99@gmail.com'}, total: 340, placed: new Date(2020, 5, 13), fulfilled: new Date(2021, 7, 9) },
    {id: 1, customer: {id: 1, name: ' 301 Watsessing Ave', state: 'NJ', email:'xyz99@gmail.com'}, total: 340, placed: new Date(2020, 5, 13), fulfilled: new Date(2021, 7, 9) },
    {id: 1, customer: {id: 1, name: ' 301 Watsessing Ave', state: 'NJ', email:'xyz99@gmail.com'}, total: 340, placed: new Date(2020, 5, 13), fulfilled: new Date(2021, 7, 9) },
    {id: 1, customer: {id: 1, name: ' 301 Watsessing Ave', state: 'NJ', email:'xyz99@gmail.com'}, total: 340, placed: new Date(2020, 5, 13), fulfilled: new Date(2021, 7, 9) },
    {id: 1, customer: {id: 1, name: ' 301 Watsessing Ave', state: 'NJ', email:'xyz99@gmail.com'}, total: 340, placed: new Date(2020, 5, 13), fulfilled: new Date(2021, 7, 9) }
  ]

  ngOnInit(): void {
  }

}
