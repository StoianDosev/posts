import { Component, OnInit, ViewChild } from '@angular/core';
import { InitializeService } from 'src/app/services/initialize.service';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/Post';
import { switchMap } from 'rxjs/operators';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {

  displayedColumns: string[] = ['favorite', 'title', 'userName', 'commentsCount', 'delete-post', 'post-details'];
  posts: Post[] = [];
  text: string = 'Reset button';
  toggleStar: boolean = false;
  dataSource = new MatTableDataSource<Post>();
  sortBy:string = '';
  sortOrder:string = '';


  @ViewChild(MatTable, { static: true }) table!: MatTable<any>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private initializeService: InitializeService,
    private postService: PostService,
    private router: Router) { }

  ngOnInit(): void {
    this.postService.getAllSorted('title', 'asc').subscribe((posts) => this.dataSource.data = posts)
  }

  ngAfterViewInit() {
    this.sort.sortChange.pipe(
      switchMap((s) => 
        this.postService.getAllSorted(s.active, s.direction)
        )
    ).subscribe((posts) => this.dataSource.data = posts)
  }

  onReload() {
    console.log("Reload...");
    this.initializeService.reloadPosts()
      .pipe(
        switchMap((message) => this.postService.getAllSorted('title', 'asc'))
      )
      .subscribe((posts) => this.dataSource.data = posts );
    console.log(this.posts);
  }

  deletePost(id: number) {
    this.postService.delete(id).pipe(
      switchMap(() => this.postService.getAllSorted('title', 'asc'))
    )
      .subscribe((posts) => this.dataSource.data = posts);
  }

  toggleFavorite(post: Post) {
    console.log("toggle..." + post.id)
    post.favorite = !post.favorite;
    this.postService.update(post).subscribe(() => true);
  }

  navigateToDetails(id: number) {
    this.router.navigate([`/post-details/${id}`])
  }
}
