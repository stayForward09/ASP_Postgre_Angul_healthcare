import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ServerMessage } from '../shared/server-message';
import { Server } from '../shared/server';

@Component({
  selector: 'app-server',
  templateUrl: './server.component.html',
  styleUrls: ['./server.component.css']
})


//c;ass
export class ServerComponent implements OnInit {

  constructor() { }

  color !: string;
  buttonText !: string;
  serverStatus !: string;
  isLoading !: boolean;

  @Input() serverInput !: Server;
  @Output() serverAction = new EventEmitter<ServerMessage>();


  ngOnInit() {
    this.setServerStatus(this.serverInput.isOnline);
  }


  // set status of servers
  setServerStatus(isOnline: boolean) {
    if (isOnline) {
      this.serverInput.isOnline = true;
      this.serverStatus = 'Online';
      this.color = '#66BB6A',
      this.buttonText = 'Shut Down';
    } else {
      this.serverInput.isOnline = false;
      this.serverStatus = 'Offline';
      this.color = '#FF6B6B';
      this.buttonText = 'Start';
    }
  }//end setServerStatus


  // server while laoding
  makeLoading() {
    this.color = '#FFCA28';
    this.buttonText = 'Pending...';
    this.isLoading = true;
    this.serverStatus = 'Loading';
  }//end makeLoading


  // send Server Action
  sendServerAction(isOnline: boolean) {
    console.log('sendServerAction called!');
    this.makeLoading();
    const payload = this.buildPayload(isOnline);
    this.serverAction.emit(payload);
  }//end sendServerAction


  // build pay load
  buildPayload(isOnline: boolean): ServerMessage {
    if (isOnline) {
      return {
        id: this.serverInput.id,
        payload: 'deactivate'
      };
    } 
    else {
      return {
        id: this.serverInput.id,
        payload: 'activate'
      };
    }//end ifelse

  }//end buildPayLoad


}//end class
