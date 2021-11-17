import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from 'src/app/services/post.service';
import { switchMap } from 'rxjs/operators';
import { PostDetails } from 'src/app/models/PostDetails';
import { Comment } from 'src/app/models/Comment';
import { CommentService } from 'src/app/services/comment.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent implements OnInit {


  postDetails!: PostDetails;
  comments: Comment[] = []
  text: string = "Reload comments";

  constructor(private postService: PostService, private commentService: CommentService, private activedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activedRoute.params
      .pipe(
        switchMap(
          (p) => this.postService.getById(p['id'])
        )
      )
      .subscribe((post) => {
        this.postDetails = post;
        this.commentService.getComments(post.id).subscribe(comments => this.comments = comments)
      });
  }

  loadComments() {
    this.activedRoute.params
      .pipe(
        switchMap(
          (p) => this.commentService.getComments(p['id'])
        )
      )
      .subscribe((comments) => {
        this.comments = comments
      });
  }

  deleteComment(comment: Comment) {
    this.commentService.delete(comment.id)
      .pipe(
        switchMap((p) => this.commentService.getComments(comment.id))
      )
      .subscribe((comments) => this.loadComments())
    console.log("deleted comment: " + comment.id);
    console.log(this.comments);
  }

  createComment(comment: Comment) {
    this.commentService.create(comment).subscribe((c) => this.comments.push(c));
  }

  getPostId(): number {
    let postId = -1;
    this.activedRoute.params.subscribe(param => postId = param['id']);
    return postId;
  }

}
