import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { Comment } from 'src/app/models/Comment';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {

  @Output() onAddComment: EventEmitter<Comment> = new EventEmitter();

  userEmail: string = '';
  userName: string = '';
  body: string = '';
  @Input() postId!: number;
  
  showAddTask: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {


    const newTask = {
      id: 0,
      userEmail: this.userEmail,
      userName: this.userName,
      body: this.body,
      postId: this.postId

    }

    this.onAddComment.emit(newTask);

    this.body = '';
    this.userEmail = '';
    this.userName = '';
  }

}
