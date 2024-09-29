import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MessageDialogComponent } from '../../dialogs/message-dialog/message-dialog.component';
import { Day } from '../../../models/day.model';
import { NewDayDialogComponent } from '../../dialogs/new-day-dialog/new-day-dialogcomponent';

@Component({
  selector: 'days-widget',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgFor, MatIconModule, NewDayDialogComponent, MessageDialogComponent],
  templateUrl: './days-widget.component.html',
  styleUrls: ['./days-widget.component.scss']
})
export class DaysWidgetComponent implements OnInit {
  days: Day[] = [];

  addRuleDialog: any;

  @Output() daysChanged = new EventEmitter<Day[]>();
  constructor(private dialog: MatDialog) { }


  ngOnInit(): void {
    this.daysChanged.emit(this.days);
  }


  openDialog(): void {
    this.addRuleDialog = this.dialog.open(NewDayDialogComponent, {
      width: '400px',
    });
    this.addRuleDialog.afterClosed().subscribe((result: any) => {
      if (result) {
        this.days.push(result);
        this.dialog.open(MessageDialogComponent, {data: { message: "Day added successfully!", title:"Success!"}});
        this.daysChanged.emit(this.days);
      }
    });
  }

  removeDayAtindex(index: number)
  {
    this.days.splice(index, 1);
    this.daysChanged.emit(this.days);
  }

}
