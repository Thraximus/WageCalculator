import { Component, Inject  } from '@angular/core';
import { MatDialogRef, MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'message-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './message-dialog.component.html',
  styleUrls: ['./message-dialog.component.scss']
})
export class MessageDialogComponent {
  message: string;
  title: string;

  constructor(
    public dialogRef: MatDialogRef<MessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { message: string, title: string }
  ) {
    this.message = data.message;
    this.title = data.title;
  }

  onOk(): void {
    this.dialogRef.close();
  }

}