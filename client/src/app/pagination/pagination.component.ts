import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {


  @Input() page !: number;
  @Input() count !: number;
  @Input() perPage !: number;
  @Input() pagesToShow !: number;
  @Input() loading !: boolean;

  @Output() goPrev = new EventEmitter<boolean>();
  @Output() goNext = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit(): void {
    
  }

  onPrev(): void{
    this.goPrev.emit(true);
  }

  onNext(): void{
    this.goNext.emit(true);
  }

}
