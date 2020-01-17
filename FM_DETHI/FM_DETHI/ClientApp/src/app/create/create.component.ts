import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from '../users.service';
import { TestService } from '../test.service';
import { Test } from '../models/test';
import { Question } from '../models/question';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {

   private thisUser: object;
   private htmlString : string;
   private curentDate: Date = new Date();
   private currentDay: string;
   private datesendDB: string = (this.curentDate.getFullYear() + '-0' + (this.curentDate.getMonth() + 1) + '-' + this.curentDate.getDate() + 'T' + this.curentDate.getHours() + ":" + this.curentDate.getMinutes() + ":" + this.curentDate.getSeconds()).toString();
   //user_id, date_create, title 
   public test = new Test(JSON.parse(sessionStorage.getItem("user")).Id,this.datesendDB,'');
   //test_code, title, answer_a, answer_b, answer_c, answer_d, answer_true
   public question_1 = new Question('','','','','','','');
   public question_2 = new Question('','','','','','','');
   public question_3 = new Question('','','','','','','');
   public question_4 = new Question('','','','','','','');
   public question_5 = new Question('','','','','','','');

  constructor(private router: Router, private _testService: TestService) { }

  ngOnInit() {
  	let loggin = sessionStorage.getItem("loggin");
  	if(!loggin){
  		this.router.navigate(['/login']);
  		sessionStorage.clear();
  	}
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
  }

  cancelForm(){
  	let accept = confirm("Bạn chắc chắn muốn huỷ?");
  	if(accept){
  		this.router.navigate(['/dashboard']);
  	}
    this.thisUser = JSON.parse(sessionStorage.getItem("user"));
  }

  onSubmit() {
    this._testService.postTest(this.test).subscribe(
                                                    data => {
                                                      this.question_1.test_code = data.test_code;
                                                      this._testService.postQuestion(this.question_1).subscribe(
                                                                                                      data => console.log("ans success"),
                                                                                                      error => console.log(JSON.stringify(error))
                                                                                                     );
                                                    },
                                                    error => console.log(JSON.stringify(error))
                                                   );
    // this._testService.postQuestion(this.question_1).subscribe(
    //                                                 data => console.log("ans success"),
    //                                                 error => console.log(JSON.stringify(error))
    //                                                );
  }

  addMoreQuestion(){

  }

}
