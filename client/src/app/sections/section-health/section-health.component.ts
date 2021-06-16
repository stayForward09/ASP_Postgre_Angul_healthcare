import { Component, OnInit, OnDestroy } from '@angular/core';
import { ServerService } from '../../services/server.service';
import { Server } from '../../shared/server';
import { ServerMessage } from '../../shared/server-message';
import { Observable } from 'rxjs/Rx';
import { AnonymousSubscription } from 'rxjs/Subscription';

// const SAMPLE_SERVERS = [
//   { id: 1, name: 'dev-web', isOnline: true },
//   { id: 2, name: 'dev-mail', isOnline: false },
//   { id: 3, name: 'prod-web', isOnline: true },
//   { id: 4, name: 'prod-mail', isOnline: true }
// ];

@Component({
  selector: 'app-section-health',
  templateUrl: './section-health.component.html',
  styleUrls: ['./section-health.component.css']
})


//class
export class SectionHealthComponent implements OnInit, OnDestroy {

  constructor(private _serverService: ServerService) { }

  servers !: Server[];
  timerSubscription !: AnonymousSubscription;


  // Init
  ngOnInit() {
    this.refreshData();
  }//end Init


  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }


  // refresh Data
  refreshData() {
    this._serverService.getServers().subscribe(res => {
      this.servers = res;
    });

    this.subscribeToData();
  }//end refreshDate


  subscribeToData() {
    this.timerSubscription = Observable.timer(5000).first().subscribe(() => this.refreshData());
  }


  // send error Message
  sendMessage(msg: ServerMessage) {
    this._serverService.handleServerMessage(msg)
      .subscribe(res => console.log('Message sent to server:', msg),
                 err => console.log('Error:', err));
  }//end sendMessage


}//end class