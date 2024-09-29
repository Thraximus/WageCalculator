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


  // Inicialises the days list in the home component, 
  // and checks if any days have been saved in the session storage
  ngOnInit(): void {
    const storedDays = sessionStorage.getItem('days');
    if (storedDays) {
      this.days = JSON.parse(storedDays);
    }
    this.daysChanged.emit(this.days);
  }

  // Opens dialog for adding new days, and on successfull add, updates the days list in the home component
  openDialog(): void {
    this.addRuleDialog = this.dialog.open(NewDayDialogComponent, {
      width: '400px',
    });
    this.addRuleDialog.afterClosed().subscribe((result: any) => {
      if (result) {
        this.days.push(result);
        this.saveDaysToLocalStorage();
        this.dialog.open(MessageDialogComponent, {data: { message: "Day added successfully!", title:"Success!"}});
        this.daysChanged.emit(this.days);
      }
    });
  }

  // Removes specified day, and updates the days list in the home component
  removeDayAtindex(index: number)
  {
    this.days.splice(index, 1);
    this.saveDaysToLocalStorage();
    this.daysChanged.emit(this.days);
  }

  // I'm using session storage, so that the data doesnt persist once the tab or window is closed.
  private saveDaysToLocalStorage(): void {
    sessionStorage.setItem('days', JSON.stringify(this.days));
  }
}
