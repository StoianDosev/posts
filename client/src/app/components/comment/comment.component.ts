import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Comment } from 'src/app/models/Comment';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

  @Input() comment!: Comment
  @Output() onDelete: EventEmitter<Comment> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  delete(){
    this.onDelete.emit();
  }

}
