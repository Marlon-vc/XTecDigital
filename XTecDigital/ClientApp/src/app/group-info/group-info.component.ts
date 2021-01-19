import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-group-info',
  templateUrl: './group-info.component.html',
  styleUrls: ['./group-info.component.css']
})
export class GroupInfoComponent implements OnInit {

  group: any;

  constructor() { }

  ngOnInit(): void {
    this.group = JSON.parse( window.localStorage.getItem('group'));
    
    if (this.group == undefined) {
      console.log('No group found!');
    }

    console.log(this.group);
    
  }

}
