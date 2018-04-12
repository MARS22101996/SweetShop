import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BasketService } from "../shared/services/basket.service";
import { Order } from "../shared/models/order";
import { PercentPipe } from '@angular/common';
import { DatePipe } from '@angular/common';
import { OrderDetails } from "../shared/models/order.detail";

@Component({
  templateUrl: './basket.component.html'
})
export class BasketComponent implements OnInit {
  order: Order;
  orderDetails: OrderDetails[];
  constructor(private router: Router, private basketService: BasketService) { }

  ngOnInit() {
    this.load();
  }

  load() {
    this.basketService.getBasket().subscribe((data: Order) => {
      this.order = data;
      this.orderDetails = data.orderDetails
    });
  }

  delete(id: number) {
    this.basketService.delete(id).subscribe(data => {
      this.load();
    });
  }

  edit(orderDetails: OrderDetails) {
    this.basketService.updateOrderDetail(orderDetails).subscribe(data => this.load());
  }

  
  payOrder(order: Order) {
    this.basketService.payOrder(order).subscribe(data => this.load());
  }

  convert() {
    let jsPDF = require('jspdf');
    require('jspdf-autotable');
    
    let pdf = new jsPDF('l', 'pt', 'a4');

    var orderDate = new Date(this.order.date);
    pdf.text(35, 25, 'Order - ' + orderDate.toLocaleDateString());

    var source = document.getElementById("basic-table");
    var res = pdf.autoTableHtmlToJson(source);

    pdf.autoTable(res.columns, res.data,
      {
        margin: { top: 80 },
        theme: 'grid',
        startY: 60,
        styles: {
          lineWidth: 0.01,
          lineColor: 0,
          fillStyle: 'DF',
          halign: 'center',
          valign: 'middle',
          columnWidth: 'auto',
          overflow: 'linebreak'
        }
      });

    pdf.save("Basket.pdf");
  }
}