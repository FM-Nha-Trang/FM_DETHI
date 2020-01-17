import { Component, OnInit } from '@angular/core'; 
import { Router } from '@angular/router';
import { UsersService } from '../users.service';
import { TestService } from '../test.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  private thisUser: object;
  private testList: any;
  public arrayTest;

  constructor(private router: Router, private _testService: TestService) { }

  ngOnInit() {
    let arrayTest = [];
  	let loggin = sessionStorage.getItem("loggin");
  	if(!loggin){
  		this.router.navigate(['/login']);
  		sessionStorage.clear();
  	}
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
    this.testList = this._testService.getTestList()
                                     .subscribe((data:any) => {arrayTest = data;}, error => console.log("failed"));
     console.log(arrayTest);
  }



}
