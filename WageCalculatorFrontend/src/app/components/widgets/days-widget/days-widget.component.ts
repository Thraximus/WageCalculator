import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from '../../dialogs/success-dialog/success-dialog.component';
import { Day } from '../../../models/day.model';
import { NewDayDialogComponent } from '../../dialogs/new-day-dialog/new-day-dialogcomponent';

@Component({
  selector: 'days-widget',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgFor, MatIconModule, NewDayDialogComponent, SuccessDialogComponent],
  templateUrl: './days-widget.component.html',
  styleUrls: ['./days-widget.component.scss']
})
export class DaysWidgetComponent implements OnInit {
  days: Day[] = [];

  addRuledialog: any;

  constructor(private dialog: MatDialog) { }


  ngOnInit(): void {
  }


  openDialog(): void {
    this.addRuledialog = this.dialog.open(NewDayDialogComponent, {
      width: '400px',
    });
    this.addRuledialog.afterClosed().subscribe((result: any) => {
      if (result) {
        this.days.push(result);
        this.dialog.open(SuccessDialogComponent, {data: { successMessage: "Day added successfully!"}});
      }
    });
  }

  removeDayAtindex(index: number)
  {
    this.days.splice(index, 1);
  }

}
