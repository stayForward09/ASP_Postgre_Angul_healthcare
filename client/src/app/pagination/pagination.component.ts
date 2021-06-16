import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})


//class
export class PaginationComponent implements OnInit {


  // Inputs
  @Input() page !: number;
  @Input() count !: number;
  @Input() perPage !: number;
  @Input() pagesToShow !: number;
  @Input() loading !: boolean;


  // Outputs
  @Output() goPrev = new EventEmitter<boolean>();
  @Output() goNext = new EventEmitter<boolean>();
  @Output() goPage = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
    
  }


  // Prvios Button
  onPrev(): void{
    this.goPrev.emit(true);
  }


  // Next Button
  onNext(): void{
    this.goNext.emit(true);
  }


  onPage(n: number): void{
    this.goPage.emit(n);
  }


  // Total number of pages
  totalPages(): number{
    return Math.ceil(this.count/this.perPage) || 0;
  }


  // Check last page
  isLastPage(): boolean{
    return this.perPage * this.page >= this.count;
  }


  getMin(): number{
    return ((this.perPage * this.page) - this.perPage) + 1;
  }



  getMax(): number{
    let max = this.perPage * this.page;
    if(max > this.count){
      max = this.count;
    }
    return max;
  }


  // get pages
  getPages(): number[]{

    const totalPages = Math.ceil(this.count / this.perPage);
    const thisPage = this.page || 1;
    const pagesToShow = this.pagesToShow || 9;
    const pages: number[] = [];

    pages.push(thisPage);

    for(let i = 0; i < pagesToShow -1; i++){

      if(pages.length < pagesToShow){
        if(Math.min.apply(null, pages) > 1){
          pages.push(Math.min.apply(null, pages) - 1);
        }
      }

      if(pages.length < pagesToShow){
        if(Math.max.apply(null, pages)< totalPages){
          pages.push(Math.max.apply(null, pages)+1)
        }
      }//end if

    }//end for

    pages.sort((a,b)=> a - b);

    return pages;
  }//end getPages


}//end class
