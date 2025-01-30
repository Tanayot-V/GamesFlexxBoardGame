using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdjustK2DFont : MonoBehaviour
{
    public List<TMP_FontAsset> fontAsset;

    void Start()
    {
        fontAsset.ForEach(o => { o.fontFeatureTable.glyphPairAdjustmentRecords.Clear(); });

            fontAsset.ForEach(o => {
            //ทิ่ ทิ้ ทิ๋ ทิ๊ ทิ๋ ทิ์ ที่ ที้ ที๋ ที๊ ทึ่ ทึ้ ทึ๋ ทึ๊ ทื่ ทื้ ทื๋ ทื๊ ทำ่ ทำ้ ท๋ำ ท๋ำ ท๊ำ ป่ ป้ป๋ ป๋ ป๊ ฝ่ ฝ้ ฝ๋ ฝ๊

            TMP_GlyphValueRecord valueZero = new TMP_GlyphValueRecord(0,0,0,0);
            TMP_GlyphValueRecord vowel_1 = new TMP_GlyphValueRecord(0, 26, 0, 0);
            TMP_GlyphValueRecord vowel_2 = new TMP_GlyphValueRecord(0, 30, 0, 0);
            TMP_GlyphValueRecord vowel_3 = new TMP_GlyphValueRecord(-40, 25, 0, 0);
            TMP_GlyphValueRecord vowel_4 = new TMP_GlyphValueRecord(0, 25, 0, 0);
            TMP_GlyphValueRecord vowel_5 = new TMP_GlyphValueRecord(-20, 0, 0, 0);


            AddGlyphAdjustmentRecord(o, 747, 730, valueZero, vowel_1); //ทิ่
            AddGlyphAdjustmentRecord(o, 747, 733, valueZero, vowel_1); //ทิ้
            AddGlyphAdjustmentRecord(o, 747, 739, valueZero, vowel_1); //ทิ๋
            AddGlyphAdjustmentRecord(o, 747, 736, valueZero, vowel_1); //ทิ๊
            AddGlyphAdjustmentRecord(o, 747, 741, valueZero, vowel_1); //ทิ์

            AddGlyphAdjustmentRecord(o, 728, 730, valueZero, vowel_2); //ทั่
            AddGlyphAdjustmentRecord(o, 728, 733, valueZero, vowel_2); //ทั้
            AddGlyphAdjustmentRecord(o, 728, 739, valueZero, vowel_2); //ทั๋
            AddGlyphAdjustmentRecord(o, 728, 736, valueZero, vowel_2); //ทั๊
            AddGlyphAdjustmentRecord(o, 728, 741, valueZero, vowel_2); //ทั์

            AddGlyphAdjustmentRecord(o, 749, 730, valueZero, vowel_2); //ที่
            AddGlyphAdjustmentRecord(o, 749, 733, valueZero, vowel_2); //ที้
            AddGlyphAdjustmentRecord(o, 749, 739, valueZero, vowel_2); //ที๋
            AddGlyphAdjustmentRecord(o, 749, 736, valueZero, vowel_2); //ที๊
            AddGlyphAdjustmentRecord(o, 749, 741, valueZero, vowel_2); //ที์

            AddGlyphAdjustmentRecord(o, 753, 730, valueZero, vowel_2); //ทื่
            AddGlyphAdjustmentRecord(o, 753, 733, valueZero, vowel_2); //ทื้
            AddGlyphAdjustmentRecord(o, 753, 739, valueZero, vowel_2); //ทื๋
            AddGlyphAdjustmentRecord(o, 753, 736, valueZero, vowel_2); //ทื๊
            AddGlyphAdjustmentRecord(o, 753, 741, valueZero, vowel_2); //ทื์

            AddGlyphAdjustmentRecord(o, 751, 730, valueZero, vowel_2); //ทึ่
            AddGlyphAdjustmentRecord(o, 751, 733, valueZero, vowel_2); //ทึ้
            AddGlyphAdjustmentRecord(o, 751, 739, valueZero, vowel_2); //ทึ๋
            AddGlyphAdjustmentRecord(o, 751, 736, valueZero, vowel_2); //ทึึ๊
            AddGlyphAdjustmentRecord(o, 751, 741, valueZero, vowel_2); //ทื์

            AddGlyphAdjustmentRecord(o, 487, 730, valueZero, vowel_3); //ท่ำ ำ ่
            AddGlyphAdjustmentRecord(o, 487, 733, valueZero, vowel_3); //ท้ำ
            AddGlyphAdjustmentRecord(o, 487, 739, valueZero, vowel_3); //ท๋ำ
            AddGlyphAdjustmentRecord(o, 487, 736, valueZero, vowel_3); //ท๊ำ


            AddGlyphAdjustmentRecord(o, 730, 487, vowel_4, valueZero); //ท่ำ ่ ำ
            AddGlyphAdjustmentRecord(o, 733, 487, vowel_4, valueZero); //ท้ำ
            AddGlyphAdjustmentRecord(o, 739, 487, vowel_4, valueZero); //ท๋ำ
            AddGlyphAdjustmentRecord(o, 736, 487, vowel_4, valueZero); //ท๊ำ

            AddGlyphAdjustmentRecord(o, 462, 730, valueZero, vowel_5); //ป่
            AddGlyphAdjustmentRecord(o, 462, 733, valueZero, vowel_5); //ป้
            AddGlyphAdjustmentRecord(o, 462, 739, valueZero, vowel_5); //ป๋
            AddGlyphAdjustmentRecord(o, 462, 736, valueZero, vowel_5); //ป๊
            AddGlyphAdjustmentRecord(o, 462, 741, valueZero, vowel_5); //ป์

            AddGlyphAdjustmentRecord(o, 464, 730, valueZero, vowel_5); //ฝ่
            AddGlyphAdjustmentRecord(o, 464, 733, valueZero, vowel_5); //ฝ้
            AddGlyphAdjustmentRecord(o, 464, 739, valueZero, vowel_5); //ฝ๋
            AddGlyphAdjustmentRecord(o, 464, 736, valueZero, vowel_5); //ฝ๊
            AddGlyphAdjustmentRecord(o, 464, 741, valueZero, vowel_5); //ฝ์

            // บันทึกการเปลี่ยนแปลงในฟอนต์
            TMPro_EventManager.ON_FONT_PROPERTY_CHANGED(true, o);
        });
    }

    void AddGlyphAdjustmentRecord(TMP_FontAsset _fontAsset, uint _firstGlyph, uint _secondGlyph, TMP_GlyphValueRecord _firstGlyphOffset, TMP_GlyphValueRecord _secondGlyphOffset)
    {
        TMP_GlyphAdjustmentRecord firstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(_firstGlyph, _firstGlyphOffset);
        TMP_GlyphAdjustmentRecord secondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(_secondGlyph, _secondGlyphOffset);

        TMP_GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);

        _fontAsset.fontFeatureTable.glyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
    }
}
