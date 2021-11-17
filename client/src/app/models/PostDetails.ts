import { Comment } from '../models/Comment';

export interface PostDetails {
    id: number;
    title: string;
    body: string;
    comments: Comment[];    
}

