using UnityEngine;
using System.Collections.Generic;

public class Laser {
    public class Segment
    {
        public Vector3 RealBegin;
        public Vector3 SkewedBegin;
        public Vector3 RealEnd;
        public Vector3 SkewedEnd;
        public float Next;

        public Segment(Vector3 realBegin, Vector3 skewedBegin, Vector3 realEnd, Vector3 skewedEnd, float next)
        {

            RealBegin = realBegin;
            SkewedBegin = skewedBegin;
            RealEnd = realEnd;
            SkewedEnd = skewedEnd;
            Next = next;
        }
    }

    public static void hide(LineRenderer lineRenderer)
    {
        lineRenderer.SetVertexCount(0);
    }

    public class FloatComparer : IComparer<float>
    {
        public int Compare(float l, float r)
        {
            return -l.CompareTo(r);
        }
    }

    public static void draw(LineRenderer lineRenderer, Vector3 origin, Vector3 direction, float length)
    {
        Vector3 end = origin + length * direction;

        Vector3 lineStart = Player.skewedPos(origin);
        Vector3 lineEnd = Player.skewedPos(end);

        if (Mathf.Approximately(0f, Vector3.Distance(lineStart, lineEnd)))
        {
            hide(lineRenderer);
        }
        else
        {
            int segments = 30;
            lineRenderer.SetVertexCount(segments + 1);

            SortedDictionary<float, Segment> segmentsInOrder = new SortedDictionary<float, Segment>();
            SortedDictionary<float, LinkedList<float>> segmentsPriorities = new SortedDictionary<float, LinkedList<float>>(new FloatComparer());

            segmentsInOrder[float.MinValue] = new Segment(origin, lineStart, end, lineEnd, float.MaxValue);      
            
            LinkedList<float> list = new LinkedList<float>();
            segmentsPriorities[float.MaxValue] = list;
            list.AddLast(float.MinValue);
            
            int done = 1;
            
            while (done < segments)
            {
                SortedDictionary<float,LinkedList<float>>.Enumerator e = segmentsPriorities.GetEnumerator();
                e.MoveNext();
                float nextSegmentToHalf = e.Current.Value.First.Value;
                if (e.Current.Value.Count == 1)
                {
                    segmentsPriorities.Remove(e.Current.Key);
                }
                else
                {
                    e.Current.Value.RemoveFirst();
                }

                Segment seg = segmentsInOrder[nextSegmentToHalf];

                Vector3 realMid = (seg.RealBegin + seg.RealEnd) * 0.5f;
                Vector3 skewedMid = Player.skewedPos(realMid);

                float nextMid = nextSegmentToHalf * 0.5f + seg.Next * 0.5f;

                Segment l = new Segment(seg.RealBegin, seg.SkewedBegin, realMid, skewedMid, nextMid);
                Segment r = new Segment(realMid, skewedMid, seg.RealEnd, seg.SkewedEnd, seg.Next);

                segmentsInOrder[nextSegmentToHalf] = l;
                segmentsInOrder[nextMid] = r;

                float lDist = Vector3.Distance(l.SkewedBegin, l.SkewedEnd);
                float rDist = Vector3.Distance(r.SkewedBegin, r.SkewedEnd);

                if(!segmentsPriorities.TryGetValue(lDist, out list)) {
                    list = new LinkedList<float>();
                    segmentsPriorities[lDist] = list;
                }
                list.AddLast(nextSegmentToHalf);

                if(!segmentsPriorities.TryGetValue(rDist, out list)) {
                    list = new LinkedList<float>();
                    segmentsPriorities[rDist] = list;
                }
                list.AddLast(nextMid);

                done++;
            }

            int i = 0;
            foreach (Segment s in segmentsInOrder.Values)
            {
                if (i == 0)
                {
                    lineRenderer.SetPosition(i++, s.SkewedBegin);
                }
                lineRenderer.SetPosition(i++, s.SkewedEnd);
            }
        }
    }
}
