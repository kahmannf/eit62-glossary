import { IGuid } from './iguid';

export interface Entry extends IGuid {
  Title: string;
  Description: string;
  References: {
    Title: string;
    Guid: string;
  }[];
}
