import { Injectable } from '@angular/core';
import { Comment } from '../models/Comment';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private url: string = "api/comments";

  constructor(private http: HttpClient) { }

  getComments(postId: number): Observable<Comment[]> {
    const getUrl = `${this.url}/${postId}`;
    return this.http.get<Comment[]>(getUrl);
  }

  delete(id:number):Observable<any>{
    const delUrl = `${this.url}/${id}`;
    return this.http.delete(delUrl, httpOptions);
  }

  create(comment:Comment):Observable<Comment>{
    const result = this.http.post<Comment>(this.url,comment,httpOptions);
    return result;
  }
}
