import { Component, OnInit } from '@angular/core';
import { ApiRequestService } from '../api-request.service';

@Component({
  selector: 'app-pick-engineer',
  templateUrl: './pick-engineer.component.html',
  styleUrls: ['./pick-engineer.component.css']
})
export class PickEngineerComponent implements OnInit {

  private newPair : any;
  private rotaQue : any;
  
  constructor(private apiService : ApiRequestService) {
    // this way, apiService will be injected from Constructor.
  }

  onClick() {
    console.log(this.apiService.makeGetRequest)
    this.apiService.makeGetRequest().subscribe(result => {
      this.newPair = result['NewPair'];
      this.rotaQue = result['RotaQue'];
      console.log(result);
    })
  }

  ngOnInit() {
  }

}
