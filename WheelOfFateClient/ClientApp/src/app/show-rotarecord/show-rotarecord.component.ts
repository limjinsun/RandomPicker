import { Component, OnInit } from "@angular/core";
import { ApiRequestService } from '../api-request.service';


@Component({
  selector: "app-show-rotarecord",
  templateUrl: "./show-rotarecord.component.html",
  styleUrls: ["./show-rotarecord.component.css"]
})
export class ShowRotarecordComponent implements OnInit {
  employees = [
    {
      id: 1,
      first_name: "Calley"
    },
    {
      id: 2,
      first_name: "Conn"
    },
    {
      id: 3,
      first_name: "Mile"
    },
    {
      id: 4,
      first_name: "Yettie"
    },
    {
      id: 5,
      first_name: "Clem"
    },
    {
      id: 6,
      first_name: "Dick"
    },
    {
      id: 7,
      first_name: "Silvester"
    },
    {
      id: 8,
      first_name: "Dawna"
    },
    {
      id: 9,
      first_name: "Deonne"
    },
    {
      id: 10,
      first_name: "Marcia"
    }
  ];

  selectedEmployee: any;
  engineer: any;
  isMale: boolean;


  constructor(private apiService : ApiRequestService) {
    // this way, apiService will be injected from Constructor.
  }

  onChange() {
    console.log(this.selectedEmployee);
    this.apiService.makeEngineerInfoGetRequest(this.selectedEmployee['id']).subscribe(result => {
      this.engineer = result;
      this.isMale = (result['gender'] === 1)? true: false;
      console.log(result);
    })
  }

  ngOnInit() {}
}
