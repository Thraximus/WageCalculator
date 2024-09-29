import { Component } from '@angular/core';
import { MatDialogRef, MatDialogModule  } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { TimeRule } from '../../../models/time-rule.model';
import { HttpClient,HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'new-time-rule-dialog',
  standalone: true,
  imports: [MatFormFieldModule, MatDialogModule , MatInputModule, MatButtonModule,HttpClientModule,FormsModule],
  templateUrl: './new-time-rule-dialog.component.html',
  styleUrls: ['./new-time-rule-dialog.component.scss']
})
export class NewTimeRuleDialogComponent {

  timeRule: TimeRule = {
    id: 0,
    name: '',
    description: '',
    regularStartTime: 9,
    nightTimeStartTime: 17,
    midnightStartTime: 22
  };

  isFormValid: boolean = true;
  timeValidationError: string = '';

  constructor(public dialogRef: MatDialogRef<NewTimeRuleDialogComponent>,private http: HttpClient) {}

  validateTimes(): boolean {
    this.timeValidationError = '';

    if (
      this.timeRule.regularStartTime < 0 ||
      this.timeRule.regularStartTime > 24 ||
      this.timeRule.nightTimeStartTime < 0 ||
      this.timeRule.nightTimeStartTime > 24 ||
      this.timeRule.midnightStartTime < 0 ||
      this.timeRule.midnightStartTime > 24
    ) {
      this.timeValidationError = 'Times must be between 0 and 24.';
      return false;
    }

    if (
      this.timeRule.regularStartTime >= this.timeRule.nightTimeStartTime ||
      this.timeRule.regularStartTime >= this.timeRule.midnightStartTime
    ) {
      this.timeValidationError =
        'Regular start time cannot be greater than or equal to night or midnight start time.';
      return false;
    }

    if (this.timeRule.nightTimeStartTime >= this.timeRule.midnightStartTime) {
      this.timeValidationError =
        'Night start time cannot be greater than or equal to midnight start time.';
      return false;
    }

    if (this.timeRule.name == "")
    {
      this.timeValidationError =
        'Name field is required';
      return false;
    }

    return true;
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if(this.validateTimes())
    {
      this.http.post('/api/time-rules/create-rule', this.timeRule).subscribe(
        response => {
          this.dialogRef.close(response);
        },
        error => {
          console.error('Error creating time rule:', error);
          this.dialogRef.close(error);
        }
      );
    } else {
      this.isFormValid = false;
    }
    
  }
}