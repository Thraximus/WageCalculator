import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TimeRule } from '../../../models/time-rule.model';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { NewTimeRuleDialogComponent } from '../../dialogs/new-time-rule-dialog/new-time-rule-dialog.component';
import { MessageDialogComponent } from '../../dialogs/message-dialog/message-dialog.component';

@Component({
  selector: 'time-rule-widget',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgFor, MatIconModule,NewTimeRuleDialogComponent, MessageDialogComponent],
  templateUrl: './time-rule-widget.component.html',
  styleUrls: ['./time-rule-widget.component.scss']
})
export class TimeRuleWidgetComponent implements OnInit {
  timeRules: TimeRule[] = [];
  selectedTimeRule: TimeRule = { id: 0, name: '', description: '', regularStartTime: 0, nightTimeStartTime: 0, midnightStartTime: 0 };
  isDropdownOpen = false;
  addRuledialog: any;

  @Output() ruleChanged= new EventEmitter<TimeRule>();

  constructor(private http: HttpClient, private dialog: MatDialog) { }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  ngOnInit(): void {
    this.fetchTimeRules();
  }

  selectTimeRule(rule: TimeRule) {
    this.selectedTimeRule = rule;
    this.isDropdownOpen = false;
    this.ruleChanged.emit(this.selectedTimeRule);
  }

  openDialog(): void {
    this.addRuledialog = this.dialog.open(NewTimeRuleDialogComponent, {
      width: '400px',
    });

    this.addRuledialog.afterClosed().subscribe((result: any) => {
      if (result && !result.error) {
        this.fetchTimeRules();
        this.dialog.open(MessageDialogComponent, {data: { message: 'Your time rule has been added successfully!', title:"Success!"}});
        console.log(result);
      }
      else if(result.error)
      {
        this.dialog.open(MessageDialogComponent, {data: { message: "There was an error while creating the new time rule, please try again later.", title:"Error"}});
      }
    });
  }

  fetchTimeRules() {
    this.http.get<TimeRule[]>('/api/time-rules')
      .subscribe((data: TimeRule[]) => {
        this.timeRules = data;
        this.selectTimeRule(data[0]);
      }, error => {
        console.error('Failed to load time rules', error);
        this.dialog.open(MessageDialogComponent, {data: { message: "There was a server error, please try again later.", title:"Error"}});
      });
  }
}
